// Copyright (c) 2025 David A. Frischknecht
//
// SPDX-License-Identifier: Apache-2.0

using System.Windows;

namespace FNDice;

public partial class MainWindow
{
	private readonly Die[] m_dice;

	public MainWindow()
	{
		InitializeComponent();
		m_dice = [m_die1, m_die2, m_die3, m_die4, m_die5];
	}

	private void BtnRoll_OnClick(object sender, RoutedEventArgs e)
	{
		foreach (var die in m_dice)
		{
			if (!die.IsSelected) die.Roll();
		}
	}
}
