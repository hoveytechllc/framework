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
versionFromGlobalJson=""
testPattern="Tests"
versionPattern="(([0-9]{1,3}\.)?([0-9]{1,3}\.)?([0-9]{1,3}\.)?([0-9]{1,3})(-\*)?)"
regexPattern="\"version\": \"$versionPattern\""

echo "** Replacing project.json versions with value from global.json"
globalJson=`cat global.json`

if [[ $globalJson =~ $regexPattern ]]; then
	versionFromGlobalJson=${BASH_REMATCH[1]}
	replacementForProjectJson="\"version\": \"$versionFromGlobalJson\""
	echo "Parsed version from global.json: $versionFromGlobalJson"
	
	for i in "${projects[@]}"
	do
		projectJsonFilePath=""
		if [[ $i =~ $testPattern ]]; then
			projectJsonFilePath="./tests/$i/project.json"
		else
			projectJsonFilePath="./src/$i/project.json"
		fi

		echo "Calculated project.json file: $projectJsonFilePath"

		sed -i -r "0,/$regexPattern/ s/$regexPattern/$replacementForProjectJson/g" $projectJsonFilePath

		for j in "${projects[@]}"
		do
			dependencyPattern="\"$j\": \"$versionPattern\""
			replacementDependency="\"$j\": \"$versionFromGlobalJson\""
			sed -i -r "s/$dependencyPattern/$replacementDependency/g" $projectJsonFilePath
		done
	done
fi

echo "*** Restore NUGET packages"
for i in "${projects[@]}"
do
	projectJsonFilePath=""
	if [[ $i =~ $testPattern ]]; then
		projectJsonFilePath="./tests/$i/project.json"
	else
		projectJsonFilePath="./src/$i/project.json"
	fi

	dotnet restore $projectJsonFilePath
	if [ $? -ne 0 ]; then 
		exit $?
	fi
done

echo "*** Running tests"
for i in "${projects[@]}"
do
	if [[ ! $i =~ $testPattern ]]; then
		continue
	fi

	dotnet test ./tests/$i/project.json
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

	dotnet pack ./src/$i/project.json -c Release -o ./build
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