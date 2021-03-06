﻿using System.Collections.Generic;
using System.IO;
using DropNet.Models;
using RestSharp;
using System;
using DropNet.Authenticators;
using DropNet.Exceptions;
using System.Threading.Tasks;

namespace DropNet
{
    public partial class DropNetClient
    {
        public Task<MetaData> GetMetaDataTask(String hash, Boolean list = false, Boolean include_deleted = false)
        {
            return GetMetaDataTask(String.Empty, hash, list, include_deleted);
        }

        public Task<MetaData> GetMetaDataTask(String path, String hash = null, Boolean list = false, Boolean include_deleted = false)
        {
            if (path != "" && !path.StartsWith("/")) path = "/" + path;

            var request = _requestHelper.CreateMetadataRequest(path, Root, hash, list, include_deleted);

            return ExecuteTask<MetaData>(ApiType.Base, request);
        }

        public Task<MetaData> RestoreTask(string rev, string path)
        {
            var request = _requestHelper.CreateRestoreRequest(rev, path, Root);

            return ExecuteTask<MetaData>(ApiType.Base, request);
        }

        public Task<List<MetaData>> SearchTask(string searchString)
        {
            return SearchTask(searchString, string.Empty);
        }

        public Task<List<MetaData>> SearchTask(string searchString, int fileLimit)
        {
            return SearchTask(searchString, string.Empty, fileLimit);
        }

        public Task<List<MetaData>> SearchTask(string searchString, string path, int fileLimit = 1000)
        {
            if (fileLimit > 1000)
                fileLimit = 1000;

            var request = _requestHelper.CreateSearchRequest(searchString, path, Root, fileLimit);

            return ExecuteTask<List<MetaData>>(ApiType.Base, request);
        }

        public Task<IRestResponse> GetFileTask(string path)
        {
            if (!path.StartsWith("/")) path = "/" + path;

            var request = _requestHelper.CreateGetFileRequest(path, Root);

            return ExecuteTask(ApiType.Content, request);
        }

        public Task<MetaData> UploadFileTask(string path, string filename, byte[] fileData, bool overwrite = true, string parentRevision = null)
        {
            if (path != "" && !path.StartsWith("/")) path = "/" + path;

            var request = _requestHelper.CreateUploadFileRequest(path, filename, fileData, Root, overwrite, parentRevision);

            return ExecuteTask<MetaData>(ApiType.Content, request);
        }

        public Task<MetaData> UploadFileTask(string path, string filename, Stream fileStream, bool overwrite = true, string parentRevision = null)
        {
            if (path != "" && !path.StartsWith("/")) path = "/" + path;

            var request = _requestHelper.CreateUploadFileRequest(path, filename, fileStream, Root, overwrite, parentRevision);

            return ExecuteTask<MetaData>(ApiType.Content, request);
        }

        public Task<IRestResponse> DeleteTask(string path)
        {
            if (path != "" && !path.StartsWith("/")) path = "/" + path;

            var request = _requestHelper.CreateDeleteFileRequest(path, Root);

            return ExecuteTask(ApiType.Base, request);
        }

        public Task<IRestResponse> CopyTask(string fromPath, string toPath)
        {
            if (!fromPath.StartsWith("/")) fromPath = "/" + fromPath;
            if (!toPath.StartsWith("/")) toPath = "/" + toPath;

            var request = _requestHelper.CreateCopyFileRequest(fromPath, toPath, Root);

            return ExecuteTask(ApiType.Base, request);
        }

        public Task<IRestResponse> CopyFromCopyRefTask(string fromCopyRef, string toPath)
        {
            if (!toPath.StartsWith("/")) toPath = "/" + toPath;

            var request = _requestHelper.CreateCopyFileFromCopyRefRequest(fromCopyRef, toPath, Root);

            return ExecuteTask(ApiType.Base, request);
        }

        public Task<IRestResponse> MoveTask(string fromPath, string toPath)
        {
            if (!fromPath.StartsWith("/")) fromPath = "/" + fromPath;
            if (!toPath.StartsWith("/")) toPath = "/" + toPath;

            var request = _requestHelper.CreateMoveFileRequest(fromPath, toPath, Root);

            return ExecuteTask(ApiType.Base, request);
        }

        public Task<MetaData> CreateFolderTask(string path, Action<MetaData> success, Action<DropboxException> failure)
        {
            if (!path.StartsWith("/")) path = "/" + path;

            var request = _requestHelper.CreateCreateFolderRequest(path, Root);

            return ExecuteTask<MetaData>(ApiType.Base, request);
        }

        public Task<ShareResponse> GetShareTask(string path, bool shortUrl = true)
        {
            if (!path.StartsWith("/")) path = "/" + path;

            var request = _requestHelper.CreateShareRequest(path, Root, shortUrl);

            return ExecuteTask<ShareResponse>(ApiType.Base, request);
        }

        public Task<ShareResponse> GetMediaTask(string path)
        {
            if (!path.StartsWith("/")) path = "/" + path;

            var request = _requestHelper.CreateMediaRequest(path, Root);

            return ExecuteTask<ShareResponse>(ApiType.Base, request);
        }

        public Task<IRestResponse> GetThumbnailTask(MetaData file)
        {
            return GetThumbnailTask(file.Path, ThumbnailSize.Small);
        }

        public Task<IRestResponse> GetThumbnailTask(MetaData file, ThumbnailSize size)
        {
            return GetThumbnailTask(file.Path, size);
        }

        public Task<IRestResponse> GetThumbnailTask(string path)
        {
            return GetThumbnailTask(path, ThumbnailSize.Small);
        }

        public Task<IRestResponse> GetThumbnailTask(string path, ThumbnailSize size)
        {
            if (!path.StartsWith("/")) path = "/" + path;

            var request = _requestHelper.CreateThumbnailRequest(path, size, Root);

            return ExecuteTask(ApiType.Content, request);
        }

        public Task<CopyRefResponse> GetCopyRefTask(string path)
        {
            if (!path.StartsWith("/")) path = "/" + path;

            var request = _requestHelper.CreateCopyRefRequest(path, Root);

            return ExecuteTask<CopyRefResponse>(ApiType.Base, request);
        }

    }
}
