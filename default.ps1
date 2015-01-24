
properties {
  $endpoint = ?? $env:WKTEndpoint 'http://localhost:5000'
  $mapApplicationId = ?? $env:WKTMapApplicationId ([guid]::NewGuid()).ToString()
  $mapAuthenticationToken = ?? $env:WKTMapAuthenticationToken 'token-goes-here'
  [string] $privateSettingsFile = '.\KingTides.Wp8.Pan\PrivateSettings.xaml'
}

task default -depends RestoreNuget, BuildPrivateSettings

task RestoreNuget {
  exec { & .\.nuget\Nuget.exe install .\.nuget\packages.config -outputdirectory packages }
  exec { & .\.nuget\Nuget.exe restore kingtides-wp8.sln }
}

task BuildPrivateSettings -precondition { -not (Test-Path $privateSettingsFile) } {
  cp .\KingTides.Wp8.Pan\PrivateSettings.xaml.template $privateSettingsFile
  xmlPoke $privateSettingsFile "//*[local-name() = 'PrivateSettings']/@Endpoint" $endpoint  
  xmlPoke $privateSettingsFile "//*[local-name() = 'PrivateSettings']/@MapApplicationId" $mapApplicationId  
  xmlPoke $privateSettingsFile "//*[local-name() = 'PrivateSettings']/@MapAuthenticationToken" $mapAuthenticationToken  
}

#alias
New-Alias "??" Coalesce

#helpers 
function Coalesce($a, $b) { if ($a -ne $null) { $a } else { $b } }

function xmlPeek($filePath, $xpath) { 
    [xml] $fileXml = gc $filePath 
    return $fileXml.SelectSingleNode($xpath).Value 
} 

function xmlPoke($filePath, $xpath, $value) { 
    [xml] $fileXml = gc $filePath 
    $node = $fileXml.SelectSingleNode($xpath) 
    if ($node) { 
        $node.Value = $value 
        $fileXml.Save($filePath)  
    } 
}