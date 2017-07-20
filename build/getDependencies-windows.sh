#!/bin/bash
# server=build.palaso.org
# project=Bloom
# build=Bloom-Default-Continuous
# root_dir=..
# Auto-generated by https://github.com/chrisvire/BuildUpdate.
# Do not edit this file by hand!

cd "$(dirname "$0")"

# *** Functions ***
force=0
clean=0

while getopts fc opt; do
case $opt in
f) force=1 ;;
c) clean=1 ;;
esac
done

shift $((OPTIND - 1))

copy_auto() {
if [ "$clean" == "1" ]
then
echo cleaning $2
rm -f ""$2""
else
where_curl=$(type -P curl)
where_wget=$(type -P wget)
if [ "$where_curl" != "" ]
then
copy_curl $1 $2
elif [ "$where_wget" != "" ]
then
copy_wget $1 $2
else
echo "Missing curl or wget"
exit 1
fi
fi
}

copy_curl() {
echo "curl: $2 <= $1"
if [ -e "$2" ] && [ "$force" != "1" ]
then
curl -# -L -z $2 -o $2 $1
else
curl -# -L -o $2 $1
fi
}

copy_wget() {
echo "wget: $2 <= $1"
f1=$(basename $1)
f2=$(basename $2)
cd $(dirname $2)
wget -q -L -N $1
# wget has no true equivalent of curl's -o option.
# Different versions of wget handle (or not) % escaping differently.
# A URL query is the only reason why $f1 and $f2 should differ.
if [ "$f1" != "$f2" ]; then mv $f2\?* $f2; fi
cd -
}


# *** Results ***
# build: Bloom-Default-Continuous (bt222)
# project: Bloom
# URL: http://build.palaso.org/viewType.html?buildTypeId=bt222
# VCS: git://github.com/BloomBooks/BloomDesktop.git [refs/heads/master]
# dependencies:
# [0] build: bloom-win32-static-dependencies (bt396)
#     project: Bloom
#     URL: http://build.palaso.org/viewType.html?buildTypeId=bt396
#     clean: false
#     revision: latest.lastSuccessful
#     paths: {"ghostscript-win32.zip!**"=>"DistFiles/ghostscript", "optipng-0.7.4-win32/optipng.exe"=>"DistFiles", "connections.dll"=>"DistFiles", "MSBuild.Community.Tasks.dll"=>"build", "MSBuild.Community.Tasks.Targets"=>"build"}
# [1] build: BloomPlayer-Master-Continuous (BPContinuous)
#     project: Bloom
#     URL: http://build.palaso.org/viewType.html?buildTypeId=BPContinuous
#     clean: false
#     revision: latest.lastSuccessful
#     paths: {"*.*"=>"DistFiles/"}
#     VCS: https://github.com/BloomBooks/BloomPlayer [refs/heads/master]
# [2] build: PortableDevices (from PodcastUtilities) (Bloom_PortableDevicesFromPodcastUtitlies)
#     project: Bloom
#     URL: http://build.palaso.org/viewType.html?buildTypeId=Bloom_PortableDevicesFromPodcastUtitlies
#     clean: false
#     revision: latest.lastSuccessful
#     paths: {"PodcastUtilities.PortableDevices.dll"=>"lib/dotnet", "PodcastUtilities.PortableDevices.pdb"=>"lib/dotnet", "Interop.PortableDeviceApiLib.dll"=>"lib/dotnet", "Interop.PortableDeviceTypesLib.dll"=>"lib/dotnet"}
#     VCS: https://github.com/BloomBooks/PodcastUtilities.git [refs/heads/master]
# [3] build: Squirrel (Bloom_Squirrel)
#     project: Bloom
#     URL: http://build.palaso.org/viewType.html?buildTypeId=Bloom_Squirrel
#     clean: false
#     revision: latest.lastSuccessful
#     paths: {"*.*"=>"lib/dotnet"}
#     VCS: https://github.com/BloomBooks/Squirrel.Windows.git [refs/heads/master]
# [4] build: YouTrackSharp (Bloom_YouTrackSharp)
#     project: Bloom
#     URL: http://build.palaso.org/viewType.html?buildTypeId=Bloom_YouTrackSharp
#     clean: false
#     revision: latest.lastSuccessful
#     paths: {"bin/YouTrackSharp.dll"=>"lib/dotnet", "bin/YouTrackSharp.pdb"=>"lib/dotnet"}
#     VCS: https://github.com/BloomBooks/YouTrackSharp.git [LinuxCompatible]
# [5] build: Bloom Help 4.0 (Bloom_Help_BloomHelp40)
#     project: Help
#     URL: http://build.palaso.org/viewType.html?buildTypeId=Bloom_Help_BloomHelp40
#     clean: false
#     revision: latest.lastSuccessful
#     paths: {"*.chm"=>"DistFiles"}
# [6] build: pdf.js (bt401)
#     project: BuildTasks
#     URL: http://build.palaso.org/viewType.html?buildTypeId=bt401
#     clean: false
#     revision: latest.lastSuccessful
#     paths: {"pdfjs-viewer.zip!**"=>"DistFiles/pdf"}
#     VCS: https://github.com/mozilla/pdf.js.git [gh-pages]
# [7] build: GeckofxHtmlToPdf-Win32-continuous (bt463)
#     project: GeckofxHtmlToPdf
#     URL: http://build.palaso.org/viewType.html?buildTypeId=bt463
#     clean: false
#     revision: latest.lastSuccessful
#     paths: {"Args.dll"=>"lib/dotnet", "GeckofxHtmlToPdf.exe"=>"lib/dotnet", "GeckofxHtmlToPdf.exe.config"=>"lib/dotnet"}
#     VCS: https://github.com/BloomBooks/geckofxHtmlToPdf [refs/heads/master]
# [8] build: NAudio continuous (bt402)
#     project: NAudio
#     URL: http://build.palaso.org/viewType.html?buildTypeId=bt402
#     clean: false
#     revision: latest.lastSuccessful
#     paths: {"NAudio.dll"=>"lib/dotnet"}
#     VCS: https://hg.codeplex.com/forks/tombogle/supportlargewavfiles2 []
# [9] build: PdfDroplet-Win-Dev-Continuous (bt54)
#     project: PdfDroplet
#     URL: http://build.palaso.org/viewType.html?buildTypeId=bt54
#     clean: false
#     revision: latest.lastSuccessful
#     paths: {"PdfDroplet.exe"=>"lib/dotnet", "PdfSharp.dll"=>"lib/dotnet"}
#     VCS: https://github.com/sillsdev/pdfDroplet [master]
# [10] build: TidyManaged-master-win32-continuous (bt349)
#     project: TidyManaged
#     URL: http://build.palaso.org/viewType.html?buildTypeId=bt349
#     clean: false
#     revision: latest.lastSuccessful
#     paths: {"*.*"=>"lib/dotnet"}
#     VCS: https://github.com/BloomBooks/TidyManaged.git [master]
# [11] build: palaso-win32-master-nostrongname Continuous (Libpalaso_PalasoWin32masterNostrongnameContinuous)
#     project: libpalaso
#     URL: http://build.palaso.org/viewType.html?buildTypeId=Libpalaso_PalasoWin32masterNostrongnameContinuous
#     clean: false
#     revision: latest.lastSuccessful
#     paths: {"SIL.BuildTasks.dll"=>"build/", "SIL.BuildTasks.AWS.dll"=>"build/", "AWSSDK.Core.dll"=>"build/", "AWSSDK.S3.dll"=>"build/", "*.dll"=>"lib/dotnet"}
#     VCS: https://github.com/sillsdev/libpalaso.git []

# make sure output directories exist
mkdir -p ../DistFiles
mkdir -p ../DistFiles/
mkdir -p ../DistFiles/ghostscript
mkdir -p ../DistFiles/pdf
mkdir -p ../Downloads
mkdir -p ../build
mkdir -p ../build/
mkdir -p ../lib/dotnet

# download artifact dependencies
copy_auto http://build.palaso.org/guestAuth/repository/download/bt396/latest.lastSuccessful/ghostscript-win32.zip ../Downloads/ghostscript-win32.zip
copy_auto http://build.palaso.org/guestAuth/repository/download/bt396/latest.lastSuccessful/optipng-0.7.4-win32/optipng.exe ../DistFiles/optipng.exe
copy_auto http://build.palaso.org/guestAuth/repository/download/bt396/latest.lastSuccessful/connections.dll ../DistFiles/connections.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/bt396/latest.lastSuccessful/MSBuild.Community.Tasks.dll ../build/MSBuild.Community.Tasks.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/bt396/latest.lastSuccessful/MSBuild.Community.Tasks.Targets ../build/MSBuild.Community.Tasks.Targets
copy_auto http://build.palaso.org/guestAuth/repository/download/BPContinuous/latest.lastSuccessful/bloomPlayer.js ../DistFiles/bloomPlayer.js
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_PortableDevicesFromPodcastUtitlies/latest.lastSuccessful/PodcastUtilities.PortableDevices.dll ../lib/dotnet/PodcastUtilities.PortableDevices.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_PortableDevicesFromPodcastUtitlies/latest.lastSuccessful/PodcastUtilities.PortableDevices.pdb ../lib/dotnet/PodcastUtilities.PortableDevices.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_PortableDevicesFromPodcastUtitlies/latest.lastSuccessful/Interop.PortableDeviceApiLib.dll ../lib/dotnet/Interop.PortableDeviceApiLib.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_PortableDevicesFromPodcastUtitlies/latest.lastSuccessful/Interop.PortableDeviceTypesLib.dll ../lib/dotnet/Interop.PortableDeviceTypesLib.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/DeltaCompressionDotNet.MsDelta.dll ../lib/dotnet/DeltaCompressionDotNet.MsDelta.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/DeltaCompressionDotNet.PatchApi.dll ../lib/dotnet/DeltaCompressionDotNet.PatchApi.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/DeltaCompressionDotNet.dll ../lib/dotnet/DeltaCompressionDotNet.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/ICSharpCode.SharpZipLib.dll ../lib/dotnet/ICSharpCode.SharpZipLib.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/ICSharpCode.SharpZipLib.xml ../lib/dotnet/ICSharpCode.SharpZipLib.xml
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/Microsoft.Data.Edm.dll ../lib/dotnet/Microsoft.Data.Edm.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/Microsoft.Data.Edm.xml ../lib/dotnet/Microsoft.Data.Edm.xml
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/Microsoft.Data.OData.dll ../lib/dotnet/Microsoft.Data.OData.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/Microsoft.Data.OData.xml ../lib/dotnet/Microsoft.Data.OData.xml
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/Microsoft.Data.Services.Client.dll ../lib/dotnet/Microsoft.Data.Services.Client.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/Microsoft.Data.Services.Client.xml ../lib/dotnet/Microsoft.Data.Services.Client.xml
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/Microsoft.Web.XmlTransform.dll ../lib/dotnet/Microsoft.Web.XmlTransform.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/Mono.Cecil.dll ../lib/dotnet/Mono.Cecil.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/NuGet.Squirrel.dll ../lib/dotnet/NuGet.Squirrel.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/Setup.exe ../lib/dotnet/Setup.exe
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/Splat.dll ../lib/dotnet/Splat.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/Squirrel.dll ../lib/dotnet/Squirrel.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/StubExecutable.exe ../lib/dotnet/StubExecutable.exe
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/SyncReleases.exe ../lib/dotnet/SyncReleases.exe
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/System.Spatial.dll ../lib/dotnet/System.Spatial.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/System.Spatial.xml ../lib/dotnet/System.Spatial.xml
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/Update-Mono.exe ../lib/dotnet/Update-Mono.exe
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/Update.com ../lib/dotnet/Update.com
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/Update.exe ../lib/dotnet/Update.exe
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/WpfAnimatedGif.dll ../lib/dotnet/WpfAnimatedGif.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/WpfAnimatedGif.xml ../lib/dotnet/WpfAnimatedGif.xml
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/WriteZipToSetup.exe ../lib/dotnet/WriteZipToSetup.exe
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/rcedit.exe ../lib/dotnet/rcedit.exe
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Squirrel/latest.lastSuccessful/signtool.exe ../lib/dotnet/signtool.exe
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_YouTrackSharp/latest.lastSuccessful/bin/YouTrackSharp.dll ../lib/dotnet/YouTrackSharp.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_YouTrackSharp/latest.lastSuccessful/bin/YouTrackSharp.pdb ../lib/dotnet/YouTrackSharp.pdb
copy_auto http://build.palaso.org/guestAuth/repository/download/Bloom_Help_BloomHelp40/latest.lastSuccessful/Bloom.chm ../DistFiles/Bloom.chm
copy_auto http://build.palaso.org/guestAuth/repository/download/bt401/latest.lastSuccessful/pdfjs-viewer.zip ../Downloads/pdfjs-viewer.zip
copy_auto http://build.palaso.org/guestAuth/repository/download/bt463/latest.lastSuccessful/Args.dll ../lib/dotnet/Args.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/bt463/latest.lastSuccessful/GeckofxHtmlToPdf.exe ../lib/dotnet/GeckofxHtmlToPdf.exe
copy_auto http://build.palaso.org/guestAuth/repository/download/bt463/latest.lastSuccessful/GeckofxHtmlToPdf.exe.config ../lib/dotnet/GeckofxHtmlToPdf.exe.config
copy_auto http://build.palaso.org/guestAuth/repository/download/bt402/latest.lastSuccessful/NAudio.dll ../lib/dotnet/NAudio.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/bt54/latest.lastSuccessful/PdfDroplet.exe ../lib/dotnet/PdfDroplet.exe
copy_auto http://build.palaso.org/guestAuth/repository/download/bt54/latest.lastSuccessful/PdfSharp.dll ../lib/dotnet/PdfSharp.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/bt349/latest.lastSuccessful/TidyManaged.dll ../lib/dotnet/TidyManaged.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/bt349/latest.lastSuccessful/TidyManaged.dll.config ../lib/dotnet/TidyManaged.dll.config
copy_auto http://build.palaso.org/guestAuth/repository/download/bt349/latest.lastSuccessful/libtidy.dll ../lib/dotnet/libtidy.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.BuildTasks.dll ../build/SIL.BuildTasks.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.BuildTasks.AWS.dll ../build/SIL.BuildTasks.AWS.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/AWSSDK.Core.dll ../build/AWSSDK.Core.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/AWSSDK.S3.dll ../build/AWSSDK.S3.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/AWSSDK.Core.dll ../lib/dotnet/AWSSDK.Core.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/AWSSDK.S3.dll ../lib/dotnet/AWSSDK.S3.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/Commons.Xml.Relaxng.dll ../lib/dotnet/Commons.Xml.Relaxng.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/Enchant.Net.dll ../lib/dotnet/Enchant.Net.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/Interop.WIA.dll ../lib/dotnet/Interop.WIA.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/Ionic.Zip.dll ../lib/dotnet/Ionic.Zip.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/Keyman7Interop.dll ../lib/dotnet/Keyman7Interop.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/KeymanLink.dll ../lib/dotnet/KeymanLink.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/L10NSharp.dll ../lib/dotnet/L10NSharp.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/Newtonsoft.Json.dll ../lib/dotnet/Newtonsoft.Json.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.Archiving.dll ../lib/dotnet/SIL.Archiving.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.BuildTasks.AWS.dll ../lib/dotnet/SIL.BuildTasks.AWS.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.BuildTasks.dll ../lib/dotnet/SIL.BuildTasks.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.Core.Desktop.dll ../lib/dotnet/SIL.Core.Desktop.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.Core.Tests.dll ../lib/dotnet/SIL.Core.Tests.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.Core.dll ../lib/dotnet/SIL.Core.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.DblBundle.Tests.dll ../lib/dotnet/SIL.DblBundle.Tests.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.DblBundle.dll ../lib/dotnet/SIL.DblBundle.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.DictionaryServices.dll ../lib/dotnet/SIL.DictionaryServices.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.Lexicon.dll ../lib/dotnet/SIL.Lexicon.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.Lift.dll ../lib/dotnet/SIL.Lift.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.Media.dll ../lib/dotnet/SIL.Media.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.Scripture.Tests.dll ../lib/dotnet/SIL.Scripture.Tests.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.Scripture.dll ../lib/dotnet/SIL.Scripture.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.TestUtilities.dll ../lib/dotnet/SIL.TestUtilities.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.Windows.Forms.DblBundle.dll ../lib/dotnet/SIL.Windows.Forms.DblBundle.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.Windows.Forms.GeckoBrowserAdapter.dll ../lib/dotnet/SIL.Windows.Forms.GeckoBrowserAdapter.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.Windows.Forms.Keyboarding.dll ../lib/dotnet/SIL.Windows.Forms.Keyboarding.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.Windows.Forms.Scripture.dll ../lib/dotnet/SIL.Windows.Forms.Scripture.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.Windows.Forms.WritingSystems.dll ../lib/dotnet/SIL.Windows.Forms.WritingSystems.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.Windows.Forms.dll ../lib/dotnet/SIL.Windows.Forms.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.WritingSystems.Tests.dll ../lib/dotnet/SIL.WritingSystems.Tests.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/SIL.WritingSystems.dll ../lib/dotnet/SIL.WritingSystems.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/Spart.dll ../lib/dotnet/Spart.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/Commons.Xml.Relaxng.dll ../lib/dotnet/Commons.Xml.Relaxng.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/Enchant.Net.dll ../lib/dotnet/Enchant.Net.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/Interop.WIA.dll ../lib/dotnet/Interop.WIA.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/Ionic.Zip.dll ../lib/dotnet/Ionic.Zip.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/Keyman7Interop.dll ../lib/dotnet/Keyman7Interop.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/KeymanLink.dll ../lib/dotnet/KeymanLink.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/L10NSharp.dll ../lib/dotnet/L10NSharp.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/Newtonsoft.Json.dll ../lib/dotnet/Newtonsoft.Json.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.Archiving.dll ../lib/dotnet/SIL.Archiving.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.BuildTasks.dll ../lib/dotnet/SIL.BuildTasks.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.Core.Desktop.Tests.dll ../lib/dotnet/SIL.Core.Desktop.Tests.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.Core.Desktop.dll ../lib/dotnet/SIL.Core.Desktop.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.Core.Tests.dll ../lib/dotnet/SIL.Core.Tests.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.Core.dll ../lib/dotnet/SIL.Core.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.DblBundle.Tests.dll ../lib/dotnet/SIL.DblBundle.Tests.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.DblBundle.dll ../lib/dotnet/SIL.DblBundle.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.DictionaryServices.dll ../lib/dotnet/SIL.DictionaryServices.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.Lexicon.dll ../lib/dotnet/SIL.Lexicon.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.Lift.dll ../lib/dotnet/SIL.Lift.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.Media.dll ../lib/dotnet/SIL.Media.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.Scripture.Tests.dll ../lib/dotnet/SIL.Scripture.Tests.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.Scripture.dll ../lib/dotnet/SIL.Scripture.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.TestUtilities.dll ../lib/dotnet/SIL.TestUtilities.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.Windows.Forms.DblBundle.dll ../lib/dotnet/SIL.Windows.Forms.DblBundle.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.Windows.Forms.GeckoBrowserAdapter.dll ../lib/dotnet/SIL.Windows.Forms.GeckoBrowserAdapter.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.Windows.Forms.Keyboarding.dll ../lib/dotnet/SIL.Windows.Forms.Keyboarding.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.Windows.Forms.Scripture.dll ../lib/dotnet/SIL.Windows.Forms.Scripture.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.Windows.Forms.WritingSystems.dll ../lib/dotnet/SIL.Windows.Forms.WritingSystems.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.Windows.Forms.dll ../lib/dotnet/SIL.Windows.Forms.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.WritingSystems.Tests.dll ../lib/dotnet/SIL.WritingSystems.Tests.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/SIL.WritingSystems.dll ../lib/dotnet/SIL.WritingSystems.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/Spart.dll ../lib/dotnet/Spart.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/icu.net.dll ../lib/dotnet/icu.net.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/irrKlang.NET4.dll ../lib/dotnet/irrKlang.NET4.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/debug/taglib-sharp.dll ../lib/dotnet/taglib-sharp.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/icu.net.dll ../lib/dotnet/icu.net.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/irrKlang.NET4.dll ../lib/dotnet/irrKlang.NET4.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/taglib-sharp.dll ../lib/dotnet/taglib-sharp.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/x64/icudt56.dll ../lib/dotnet/icudt56.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/x64/icuin56.dll ../lib/dotnet/icuin56.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/x64/icuuc56.dll ../lib/dotnet/icuuc56.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/x86/icudt56.dll ../lib/dotnet/icudt56.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/x86/icuin56.dll ../lib/dotnet/icuin56.dll
copy_auto http://build.palaso.org/guestAuth/repository/download/Libpalaso_PalasoWin32masterNostrongnameContinuous/latest.lastSuccessful/x86/icuuc56.dll ../lib/dotnet/icuuc56.dll
# extract downloaded zip files
unzip -uqo ../Downloads/ghostscript-win32.zip -d ../DistFiles/ghostscript
unzip -uqo ../Downloads/pdfjs-viewer.zip -d ../DistFiles/pdf
# End of script
