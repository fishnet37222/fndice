// Copyright (c) 2024 David A. Frischknecht
//
// SPDX-License-Identifier: Apache-2.0

using System.Windows;

namespace FNDice;

public partial class MainWindow
{
	private Die[] m_dice = new Die[5];
	public MainWindow()
	{
		InitializeComponent();
	}

	private void BtnRollDice_OnClick(object sender, RoutedEventArgs e)
	{
		foreach (var die in m_dice)
		{
			if (die.IsSelected) continue;
			die.Roll();
		}
	}

	private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
	{
		var dice = m_grdDice.Children.OfType<Die>().ToArray();
		m_dice = dice;
	}
}