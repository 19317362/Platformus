﻿// Copyright © 2017 Dmitry Sikorsky. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using Platformus.Barebone;
using Platformus.Barebone.Backend.ViewModels;
using Platformus.Barebone.Backend.ViewModels.Shared;
using Platformus.Designers.Backend.ViewModels.Shared;

namespace Platformus.Designers.Backend.ViewModels.Bundles
{
  public class IndexViewModelFactory : ViewModelFactoryBase
  {
    public IndexViewModelFactory(IRequestHandler requestHandler)
      : base(requestHandler)
    {
    }

    public IndexViewModel Create(string orderBy, string direction, int skip, int take, string filter)
    {
      string bundlesPath = PathManager.GetBundlesPath(this.RequestHandler);
      
      return new IndexViewModel()
      {
        Grid = new GridViewModelFactory(this.RequestHandler).Create(
          orderBy, direction, skip, take, FileSystemRepository.CountFiles(bundlesPath, "*.json", filter),
          new[] {
            new GridColumnViewModelFactory(this.RequestHandler).Create("Filename", "Filename"),
            new GridColumnViewModelFactory(this.RequestHandler).CreateEmpty()
          },
          FileSystemRepository.GetFiles(bundlesPath, "*.json", filter, orderBy, direction, skip, take).Select(fi => new BundleViewModelFactory(this.RequestHandler).Create(fi)),
          "_Bundle"
        )
      };
    }
  }
}