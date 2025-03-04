// Copyright (c) 2025 David A. Frischknecht
//
// SPDX-License-Identifier: Apache-2.0

using System.ComponentModel;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using JetBrains.Annotations;

namespace FNDice;

[PublicAPI]
public class Die : Control
{
	private static readonly Random s_random = new(RandomNumberGenerator.GetInt32(int.MaxValue));

	private static readonly DependencyPropertyKey s_valuePropertyKey =
		DependencyProperty.RegisterReadOnly(nameof(Value), typeof(int), typeof(Die), new(0));
	public static readonly DependencyProperty ValueProperty = s_valuePropertyKey.DependencyProperty;
	public static readonly DependencyProperty PipSpacingProperty = DependencyProperty.Register(nameof(PipSpacing), typeof(GridLength), typeof(Die), new(new GridLength(5)));
	public static readonly DependencyProperty PipDiameterProperty = DependencyProperty.Register(nameof(PipDiameter), typeof(GridLength), typeof(Die), new(new GridLength(20)));
	public static readonly DependencyProperty BorderCornerRadiusProperty =
		DependencyProperty.Register(nameof(BorderCornerRadius), typeof(CornerRadius), typeof(Die), new(new CornerRadius(10)));
	private static readonly DependencyPropertyKey s_isSelectedPropertyKey =
		DependencyProperty.RegisterReadOnly(nameof(IsSelected), typeof(bool), typeof(Die), new(false));
	public static readonly DependencyProperty IsSelectedProperty = s_isSelectedPropertyKey.DependencyProperty;
	public static readonly DependencyProperty SelectedBackgroundProperty =
		DependencyProperty.Register(nameof(SelectedBackground), typeof(Brush), typeof(Die), new(Brushes.Black));
	public static readonly DependencyProperty SelectedForegroundProperty =
		DependencyProperty.Register(nameof(SelectedForeground), typeof(Brush), typeof(Die), new(Brushes.White));
	public static readonly DependencyProperty DisabledBackgroundProperty =
		DependencyProperty.Register(nameof(DisabledBackground), typeof(Brush), typeof(Die), new(Brushes.LightGray));
	public static readonly DependencyProperty DisabledForegroundProperty =
		DependencyProperty.Register(nameof(DisabledForeground), typeof(Brush), typeof(Die), new(Brushes.DarkGray));

	static Die()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(Die), new FrameworkPropertyMetadata(typeof(Die)));
		BorderThicknessProperty.OverrideMetadata(typeof(Die), new FrameworkPropertyMetadata(new Thickness(3)));
		BorderBrushProperty.OverrideMetadata(typeof(Die), new FrameworkPropertyMetadata(Brushes.Black));
		BackgroundProperty.OverrideMetadata(typeof(Die), new FrameworkPropertyMetadata(Brushes.White));
	}

	[Browsable(false)]
	public int Value
	{
		get => (int)GetValue(ValueProperty);

		private set => SetValue(s_valuePropertyKey, value);
	}

	[Category("Layout"), DefaultValue(typeof(GridLength), "5")]
	public GridLength PipSpacing
	{
		get => (GridLength)GetValue(PipSpacingProperty);

		set => SetValue(PipSpacingProperty, value);
	}

	[Category("Layout"), DefaultValue(typeof(GridLength), "20")]
	public GridLength PipDiameter
	{
		get => (GridLength)GetValue(PipDiameterProperty);

		set => SetValue(PipDiameterProperty, value);
	}

	[Category("Appearance"), DefaultValue(typeof(CornerRadius), "10")]
	public CornerRadius BorderCornerRadius
	{
		get => (CornerRadius)GetValue(BorderCornerRadiusProperty);

		set => SetValue(BorderCornerRadiusProperty, value);
	}

	[Browsable(false)]
	public bool IsSelected
	{
		get => (bool)GetValue(IsSelectedProperty);

		private set => SetValue(s_isSelectedPropertyKey, value);
	}

	[Category("Brush"), DefaultValue(typeof(Brush), nameof(Brushes.Black))]
	public Brush SelectedBackground
	{
		get => (Brush)GetValue(SelectedBackgroundProperty);

		set => SetValue(SelectedBackgroundProperty, value);
	}

	[Category("Brush"), DefaultValue(typeof(Brush), nameof(Brushes.White))]
	public Brush SelectedForeground
	{
		get => (Brush)GetValue(SelectedForegroundProperty);

		set => SetValue(SelectedForegroundProperty, value);
	}

	[Category("Brush"), DefaultValue(typeof(Brush), nameof(Brushes.LightGray))]
	public Brush DisabledBackground
	{
		get => (Brush)GetValue(DisabledBackgroundProperty);

		set => SetValue(DisabledBackgroundProperty, value);
	}

	[Category("Brush"), DefaultValue(typeof(Brush), nameof(Brushes.DarkGray))]
	public Brush DisabledForeground
	{
		get => (Brush)GetValue(DisabledForegroundProperty);

		set => SetValue(DisabledForegroundProperty, value);
	}

	public void Clear()
	{
		Value = 0;
	}

	public void Roll()
	{
		Value = s_random.Next(6) + 1;
	}

	protected override void OnMouseUp(MouseButtonEventArgs e)
	{
		base.OnMouseUp(e);
		IsSelected = !IsSelected;
	}
}
