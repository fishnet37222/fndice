// Copyright (c) 2024 David A. Frischknecht
//
// SPDX-License-Identifier: Apache-2.0

using System.ComponentModel;
using System.Security.Cryptography;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace FNDice;

public class Die : Control
{
	public static readonly DependencyProperty PipSpacingProperty = DependencyProperty.Register(nameof(PipSpacing),
		typeof(GridLength), typeof(Die), new(new GridLength(5)));
	public static readonly DependencyProperty PipDiameterProperty = DependencyProperty.Register(nameof(PipDiameter),
		typeof(GridLength), typeof(Die), new(new GridLength(20)));
	public static readonly DependencyProperty BorderCornerRadiusProperty =
		DependencyProperty.Register(nameof(BorderCornerRadius), typeof(CornerRadius), typeof(Die), new(new CornerRadius(10)));
	public static readonly DependencyProperty DisabledForegroundProperty =
		DependencyProperty.Register(nameof(DisabledForeground), typeof(Brush), typeof(Die), new(Brushes.DarkGray));
	public static readonly DependencyProperty DisabledBackgroundProperty =
		DependencyProperty.Register(nameof(DisabledBackground), typeof(Brush), typeof(Die), new(Brushes.LightGray));
	public static readonly DependencyProperty SelectedForegroundProperty =
		DependencyProperty.Register(nameof(SelectedForeground), typeof(Brush), typeof(Die), new(Brushes.White));
	public static readonly DependencyProperty SelectedBackgroundProperty =
		DependencyProperty.Register(nameof(SelectedBackground), typeof(Brush), typeof(Die), new(Brushes.Black));
	private static readonly DependencyPropertyKey s_isSelectedPropertyKey =
		DependencyProperty.RegisterReadOnly(nameof(IsSelected), typeof(bool), typeof(Die), new(false));
	public static readonly DependencyProperty IsSelectedProperty = s_isSelectedPropertyKey.DependencyProperty;
	private static readonly DependencyPropertyKey s_valuePropertyKey =
		DependencyProperty.RegisterReadOnly(nameof(Value), typeof(int), typeof(Die), new(0));
	public static readonly DependencyProperty ValueProperty = s_valuePropertyKey.DependencyProperty;

	private static readonly Random s_rng = new(RandomNumberGenerator.GetInt32(int.MinValue, int.MaxValue));

	static Die()
	{
		DefaultStyleKeyProperty.OverrideMetadata(typeof(Die), new FrameworkPropertyMetadata(typeof(Die)));
		ForegroundProperty.OverrideMetadata(typeof(Die), new FrameworkPropertyMetadata(Brushes.Black));
		BackgroundProperty.OverrideMetadata(typeof(Die), new FrameworkPropertyMetadata(Brushes.White));
		BorderBrushProperty.OverrideMetadata(typeof(Die), new FrameworkPropertyMetadata(Brushes.Black));
		BorderThicknessProperty.OverrideMetadata(typeof(Die), new FrameworkPropertyMetadata(new Thickness(3.0)));
	}

	[Category("Layout"), DefaultValue(5)]
	public GridLength PipSpacing
	{
		get => (GridLength)GetValue(PipSpacingProperty);

		set => SetValue(PipSpacingProperty, value);
	}

	[Category("Layout"), DefaultValue(20)]
	public GridLength PipDiameter
	{
		get => (GridLength)GetValue(PipDiameterProperty);

		set => SetValue(PipDiameterProperty, value);
	}

	[Category("Appearance"), DefaultValue(10)]
	public CornerRadius BorderCornerRadius
	{
		get => (CornerRadius)GetValue(BorderCornerRadiusProperty);

		set => SetValue(BorderCornerRadiusProperty, value);
	}

	[Category("Brush"), DefaultValue(typeof(Brush), nameof(Brushes.DarkGray))]
	public Brush DisabledForeground
	{
		get => (Brush)GetValue(DisabledForegroundProperty);

		set => SetValue(DisabledForegroundProperty, value);
	}

	[Category("Brush"), DefaultValue(typeof(Brush), nameof(Brushes.LightGray))]
	public Brush DisabledBackground
	{
		get => (Brush)GetValue(DisabledBackgroundProperty);

		set => SetValue(DisabledBackgroundProperty, value);
	}

	[Category("Brush"), DefaultValue(typeof(Brush), nameof(Brushes.White))]
	public Brush SelectedForeground
	{
		get => (Brush)GetValue(SelectedForegroundProperty);

		set => SetValue(SelectedForegroundProperty, value);
	}

	[Category("Brush"), DefaultValue(typeof(Brush), nameof(Brushes.Black))]
	public Brush SelectedBackground
	{
		get => (Brush)GetValue(SelectedBackgroundProperty);

		set => SetValue(SelectedBackgroundProperty, value);
	}

	[Browsable(false), DefaultValue(false)]
	public bool IsSelected
	{
		get => (bool)GetValue(IsSelectedProperty);

		private set => SetValue(s_isSelectedPropertyKey, value);
	}

	public int Value
	{
		get => (int)GetValue(ValueProperty);

		private set => SetValue(s_valuePropertyKey, value);
	}

	protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
	{
		if (!IsEnabled) return;

		IsSelected = !IsSelected;
	}

	public void Roll()
	{
		var newValue = s_rng.Next(1, 7);

		Value = newValue;
	}

	public void Clear()
	{
		Value = 0;
		IsSelected = false;
	}
}
