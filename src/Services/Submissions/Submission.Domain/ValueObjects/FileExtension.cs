using System;
using Articles.Abstractions.Enums;
using Blocks.Core;
using Blocks.Domain.ValueObjects;
using Submission.Domain.Entities;

namespace Submission.Domain.ValueObjects;

public class FileExtension : StringValueObject
{
    private   FileExtension(string value) => Value = value;

    public static FileExtension FromFileName(string fileName, AssetTypeDefinition assetType)
    {
        var extension = System.IO.Path.GetExtension(fileName).Remove(0, 1); // removing the '.'
        Guard.ThrowIfNullOrWhiteSpace(extension);
        Guard.ThrowIfNotEqual(
            assetType.AllowedFileExtensions.IsValidExtension(extension), true);
        
        return new FileExtension(extension);
    }
}