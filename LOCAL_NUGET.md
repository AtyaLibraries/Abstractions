# Local NuGet Feed

Use a local folder as a NuGet feed when testing Atya packages before publishing them to a remote package source.

## Publish packages to a folder

From the repository root:

```powershell
powershell -ExecutionPolicy Bypass -File .\Publish-LocalNuGet.ps1
```

The script discovers packable projects under `src`, runs `dotnet pack`, and writes the generated `.nupkg` files to `$env:ATYA_LOCAL_NUGET_FEED` when it is set, otherwise to a user-local feed:

```text
%USERPROFILE%\.nuget\local-atya
```

You can still provide another folder:

```powershell
powershell -ExecutionPolicy Bypass -File .\Publish-LocalNuGet.ps1 -FeedPath C:\NuGetLocal
```

To publish only specific projects:

```powershell
powershell -ExecutionPolicy Bypass -File .\Publish-LocalNuGet.ps1 `
  -FeedPath C:\NuGetLocal `
-Project .\src\Abstractions.NuGet\Abstractions.NuGet.csproj
```

## Register the folder as a NuGet source

You can register the folder once:

```powershell
dotnet nuget add source C:\NuGetLocal --name AtyaLocal
```

Or let the publish script register it:

```powershell
powershell -ExecutionPolicy Bypass -File .\Publish-LocalNuGet.ps1 -AddSource
```

## Notes

- The local feed is just a folder containing `.nupkg` files.
- Re-run the script after changing package code.
- The script does not change package versions.
- Before packing, the script removes the old `.nupkg` and `.snupkg` for the same package/version from the local feed folder.
- If the package version does not change, a consuming project may still keep using the cached package from the NuGet global packages cache. Clear the relevant package from the cache before restoring again.
