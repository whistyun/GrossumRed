param(
    $Flag
)

$pkgproj="Package"
$exeproj="GrossumRed"
$workdir=$pkgproj + "/obj"
$outdir=$pkgproj + "/bin"

if(Test-Path $workdir ){
    rm -Recurse -Force $workdir
}
if( -not ( Test-Path $outdir ) ){
    mkdir $outdir
}
mkdir $workdir
mkdir ${workdir}/build

dotnet build $exeproj -c Release
copy ${exeproj}/bin/Release/* ${workdir}/build -Recurse
copy ${pkgproj}/readme.txt $workdir
copy ${pkgproj}/GrossumRed.nuspec $workdir
copy ${pkgproj}/GrossumRed.targets ${workdir}/build


$version=(Select-String -Path ${pkgproj}/GrossumRed.nuspec -Pattern "<Version>(.+)</Version>").Matches.Groups[1].Value

if($Flag -ne "Release"){
    $no=Get-ChildItem ${outdir}/GrossumRed.${version}-* | Measure-Object | %{$_.Count+1}
    $version = $version + "-a" + $no
}

nuget pack ${workdir}/GrossumRed.nuspec -NoPackageAnalysis -NonInteractive -OutputDir $outdir -Version $version