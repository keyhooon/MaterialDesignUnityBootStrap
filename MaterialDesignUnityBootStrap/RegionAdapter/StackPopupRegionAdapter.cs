using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using CompositeContentNavigator;
using MaterialDesignThemes.Wpf;
using Prism.Regions;

namespace MaterialDesignUnityBootStrap.RegionAdapter
{
    public class StackPopupRegionAdapter : RegionAdapterBase<StackPanel>
    {
        private readonly Dictionary<UIElement, (Popup, Button)> _elementDictionary;
        public StackPopupRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {
            _elementDictionary= new Dictionary<UIElement, (Popup, Button)>();
        }

        protected override IRegion CreateRegion()
        {
            return (IRegion)new AllActiveRegion();
        }

        protected override void Adapt(IRegion region, StackPanel regionTarget)
        {
            region.Views.CollectionChanged += (sender, e) =>
            {
                switch (e.Action)
                {
                    case NotifyCollectionChangedAction.Add when e.NewItems == null:
                        return;
                    case NotifyCollectionChangedAction.Add:
                        {
                            foreach (UIElement newItem in e.NewItems)
                            {
                                var content = new Card()
                                {
                                    UniformCornerRadius = 5,
                                    Margin = new Thickness(10),
                                    Content = new ColorZone()
                                    {
                                        VerticalAlignment = VerticalAlignment.Stretch,
                                        Mode = ColorZoneMode.PrimaryMid,
                                        Content = newItem
                                    }
                                };
                                ShadowAssist.SetShadowDepth(content,ShadowDepth.Depth5);
                                var button = new Button()
                                {
                                    Margin = new Thickness(5),
                                    Padding = new Thickness(5),
                                    Content = new PackIcon()
                                    {
                                        Kind = ViewManager.GetHeaderPackIcon(newItem),
                                        Width = 24,
                                        Height = 24
                                    },
                                };
                                var popup = new Popup()
                                {
                                    Margin = new Thickness(5),
                                    AllowsTransparency = true,
                                    HorizontalOffset = 5,
                                    Placement = PlacementMode.Top,
                                    PlacementTarget = button,
                                    PopupAnimation = PopupAnimation.Slide,
                                    StaysOpen = false,
                                    Child = content
                                };
                                _elementDictionary.Add(newItem, (popup, button));
                                button.Command = new Prism.Commands.DelegateCommand(() => popup.IsOpen = true);
                                regionTarget.Children.Add(button);
                            }

                            break;
                        }
                    case NotifyCollectionChangedAction.Remove when e.OldItems == null:
                        return;
                    case NotifyCollectionChangedAction.Remove:
                        {
                            foreach (UIElement oldItem in e.OldItems)
                            {
                                regionTarget.Children.Remove(_elementDictionary[oldItem].Item2);
                                _elementDictionary.Remove(oldItem);
                            }

                            break;
                        }
                }
            };
        }
    
}
}
