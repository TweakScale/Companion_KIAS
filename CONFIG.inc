source ../CONFIG.inc

PACKAGE=TweakScaleCompanion.KIS
TARGETDIR="TweakScaleCompanion/KIS"
TARGETBINDIR="$TARGETDIR/Plugins"
PROJECTSDIR="$PACKAGE/"
DLLS="TweakScaleCompanion_KIS TweakScalerKISInventory"

PROJECT_BRANCH=`git branch | grep \* | cut -d ' ' -f2`
if [ "$PROJECT_BRANCH" == "master" ] ; then
	# Release mode
	PROJECT_STATE=BETA
else
	# PreRelease mode
	PROJECT_STATE=BETA
fi

VERSION=$( cat $PACKAGE.version | tr '\n' ' ' | sed -n -E 's/^.*?"VERSION\":\{.*"MAJOR":([0-9]+?),.*?"MINOR":([0-9]+?),.*?"PATCH":([0-9]+?),.*?"BUILD":([0-9]+?).*?\}.*$/\1.\2.\3.\4/p' )
