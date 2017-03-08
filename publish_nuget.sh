#!/bin/bash
declare -a projects=("HoveyTech.Autofac" \
				"HoveyTech.Core" \
				"HoveyTech.Core.EfCore" \
				"HoveyTech.Data.EfCore" \
				"HoveyTech.Data.EfCore.Autofac" \
				"HoveyTech.Core.EfCore.Tests" \
				"HoveyTech.Core.Tests" \
				"HoveyTech.Data.EfCore.Tests")
			
echo "***"	
for i in "${projects[@]}"
	do
		echo "*** Setup to use project: $i"
	done
echo "***"

#
# Clear build directory
#
rm ./build/*.*

#
# Parse version from global.json and use for each project.json
#

# setup variables
testPattern="Tests"
versionPattern="(([0-9]{1,3}\.)?([0-9]{1,3}\.)?([0-9]{1,3}\.)?([0-9]{1,3})(-\*)?)"
csprojVersionPattern="<VersionPrefix>$versionPattern<\/VersionPrefix>"

echo "** Replacing project.json versions with value from global.json"
globalVersion=`cat ./VERSION`

replacementForCsProj="<VersionPrefix>$globalVersion<\/VersionPrefix>"
echo "Using global version $globalVersion"

for i in "${projects[@]}"
do
	csprojFilePath=""
	if [[ $i =~ $testPattern ]]; then
		csprojFilePath="./tests/$i/$i.csproj"
	else
		csprojFilePath="./src/$i/$i.csproj"
	fi

	sed -i -r "s/$csprojVersionPattern/$replacementForCsProj/g" $csprojFilePath
done

echo "*** Running tests"
for i in "${projects[@]}"
do
	if [[ ! $i =~ $testPattern ]]; then
		continue
	fi

	dotnet test ./tests/$i/$i.csproj
	if [ $? -ne 0 ]; then 
		exit $?
	fi
done

echo "*** Packing projects"
for i in "${projects[@]}"
do
	if [[ $i =~ $testPattern ]]; then
		continue
	fi

	dotnet pack -c Release -o ./../../build ./src/$i/$i.csproj 
	if [ $? -ne 0 ]; then 
		exit $? 
	fi
done

echo "*** Publishing to nuget"
for i in `find ./build/*.nupkg ! -name "*.symbols.*"`
do 
	if [[ $i =~ $testPattern ]]; then
		continue
	fi

	# HOVEYTECH_NUGET_KEY s/b environment variable
	nuget push $i -apikey $HOVEYTECH_NUGET_KEY -source https://www.nuget.org/api/v2/package
	if [ $? -ne 0 ]; then 
		exit $? 
	fi
done