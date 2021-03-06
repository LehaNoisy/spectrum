﻿using System;
using System.Collections.Generic;
using System.Linq;
using Windows.UI;
using Caliburn.Micro;
using Spectrum.Demo.Services;
using Spectrum.Universal;
using WinColor = Windows.UI.Color;

namespace Spectrum.Demo.ViewModels.Editors
{
    public class CustomSchemeEditorViewModel : SchemeEditorViewModelBase
    {
        public CustomSchemeEditorViewModel(IWindowManager windowManager)
            : base(windowManager)

        {
            Colors = new BindableCollection<ColourViewModel>();
            Colors.CollectionChanged += (s, e) => NotifyOfPropertyChange(() => CanSave);
        }

        public BindableCollection<ColourViewModel> Colors
        {
            get; private set;
        } 

        public void Add(WinColor color)
        {
            Colors.Add(new ColourViewModel { Color = color });
        }

        public void Remove(ColourViewModel color)
        {
            Colors.Remove(color);
        }

        public override IEnumerable<Color.RGB> GetColours()
        {
            return Colors.Select(c => c.Color.FromSystemColor());
        }

        public override void SetColors(IEnumerable<Color.RGB> colours)
        {
            Colors.AddRange(colours.Select(c => new ColourViewModel { Color = c.ToSystemColor(255) }));
        }

        public override bool CanSave
        {
            get { return Colors.Any(); }
        }
    }
}
