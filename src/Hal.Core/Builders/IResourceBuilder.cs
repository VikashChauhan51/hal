﻿namespace Hal.Core.Builders;
public interface IResourceBuilder<TData>
{
    IResourceBuilder<TData> AddLink(string rel, string href, HttpVerbs method);
    IResourceBuilder<TData> AddEmbeddedResource<TEmbedded>(string rel, IEmbeddedResource<TEmbedded> embeddedResource);
    IResource<TData> Build();
}


public interface IResourceBuilder<TData, TMeta>
{
    IResourceBuilder<TData, TMeta> AddLink(string rel, string href, HttpVerbs method);
    IResourceBuilder<TData, TMeta> AddEmbeddedResource<TEmbedded>(string rel, IEmbeddedResource<TEmbedded> embeddedResource);
    IResource<TData, TMeta> Build();
}
