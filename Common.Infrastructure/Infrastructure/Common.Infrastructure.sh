#!/bin/bash

LAST=$(ls -t Common.Infrastructure/Infrastructure/src/bin/Release | head -n1)
echo "The most recently created file is $LAST"

nuget push -Source jopalesha -ApiKey AzureDevOps Common.Infrastructure/Infrastructure/src/bin/Release/$LAST