#!/bin/bash
declare -a projects=("HoveyTech.Autofac" \
				"HoveyTech.Core" \
				"HoveyTech.Core.EfCore" \
				"HoveyTech.Data.EfCore" \
				"HoveyTech.Data.EfCore.Autofac")
			
echo "***"	
for i in "${projects[@]}"
	do
		echo "*** Setup to use project: $i"
	done
echo "***"

#
#
#
rm ./build/*.*

#
# Parse version from global.json and use for each project.json
#

echo "** Replacing project.json versions with value from global.json"
globalJson=`cat global.json`
versionFromGlobalJson=""

regexPattern="\"version\": \"(([0-9]{1,3}\.)?([0-9]{1,3}\.)?([0-9]{1,3}\.)?([0-9]{1,3})(-\*)?)\""
if [[ $globalJson =~ $regexPattern ]]; then
	versionFromGlobalJson=${BASH_REMATCH[1]}
	replacementForProjectJson="\"version\": \"$versionFromGlobalJson\""
	echo "Parsed version from global.json: $versionFromGlobalJson"

	for i in "${projects[@]}"
	do
		sed -i -r "s/$regexPattern/$replacementForProjectJson/g" ./src/$i/project.json
	done
fi

for i in "${projects[@]}"
do
	dotnet pack ./src/$i/project.json -c Release -o ./build
done

for i in `find ./build/*.nupkg ! -name "*.symbols.*"`
do 
  nuget push $i -apikey $HOVEYTECH_NUGET_KEY -source https://www.nuget.org/api/v2/package
done


