   M S B A M L   `   `   `  ����  Valheim_Online_Launcher(    "Valheim_Online_Launcher.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   Aav2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     6G     � Title$  Launcher��5   	   $	��450��5   	   $	��950��5   	    � Cursor$ Arrow��5   	    � 
ResizeMode$ NoResize=�5	   	    � WindowStartupLocation$ CenterScreen=�5
   	    � WindowStyle$
 None=�5   	    � AllowsTransparency .  J� Opacity$ 1Q�5       � Icon$ Resources/favicon.ico��6+   ��6	   Z� .��5      � -   5        $��0,0,0,0q�5      ��5   
   �� 5       �� ImageSource$! /Resources/launcher_bg2.bmp��6   .+�5   
   �� -   5        	 ��Name$	 button��5      $��897,19.25,0,0q�6!   $	��Top=�68   $��13��6P   $��0q�6\   $
��Left=�6p   $��11��6�   
 J� IsHitTestVisible
 . $
 Hand��6�   ��5      �� 5       �� 
TargetType" ��6"   .R�5      �� .+�5      ��  ��Name$ Close2��6    $��Resources/x_hover.png��60   $
��Fill=�6O   $y�	Collapsed=�6^   �� 5      $ Close1��6    $��Resources/x.png��60   $
��Fill=�6I   $y�Visible=�6X   5       �� Triggers 5      P� 5       P� Property �@J�IsMouseOver P� Value . . �5      ��  �� 
TargetName$ Close2��6%    �� Property � y� �� Value$ Visible=�6O   �� 5      $ Close1��6%    � y�$ 	Collapsed=�6O   5      5       5"      �� 5$   
   -   5        $	 StartGameBtn��5$      $��Center=�6'   $��0,0,0,20q�6E   $��Bottom=�6W   $	��261��6r   $��81��6~   ��6�   Z� ��5%      �� 5&      " ��6"   .R�5'      �� .+�5(      �� $ Close2��6    $*��$Resources/play_now_button_hover3.png��60   $
��Fill=�6^   $y�	Collapsed=�6m   �� 5)      $ Close1��6    $$��Resources/play_now_button3.png��60   $
��Fill=�6X   $y�Visible=�6g   5*       5+      P� 5,       �@J�IsMouseOver . . �5-      �� $ Close2��6%    � y�$ Visible=�6O   �� 5.      $ Close1��6%    � y�$ 	Collapsed=�6O   5/      51      53      �� 54   
   -   5        $	 minimaze_btn��54      $��881,19.25,0,0q�6'   $	��Top=�6>   $��13��6V   $��0q�6b   $
��Left=�6v   $��11��6�   
 . $
 Hand��6�    J� RenderTransformOrigin$ 2.656,2.167&�6�   ��55      �� 56      " ��6"   .R�57      �� .+�58      �� $ Close2��6    $��Resources/minimaze.bmp��60   $
��Fill=�6P   $y�	Collapsed=�6_   �� 59      $ Close1��6    $"��Resources/minimaze_hover.bmp��60   $
��Fill=�6V   $y�Visible=�6e   5:       5;      P� 5<       �@J�IsMouseOver . . �5=      �� $ Close2��6%    � y�$ Visible=�6O   �� 5>      $ Close1��6%    � y�$ 	Collapsed=�6O   5?      5A      5C      �� 5E   
   -   5        $ _empty��5E      $��Center=�6    $��34��6=   $��	0,298,0,0q�6I   $	��Top=�6\   $%��Resources/progressbar_empty.bmp��6t   $
��Fill=�6�   $	��586��6�   $��10��6�   $��20��6�   $��587.800��6�   �� 5G   
   -   5        $ progressbar��5G      $��Center=�6%   $��34��6B   $��	0,298,0,0q�6N   $	��Top=�6a   $!��Resources/progressbar_1.bmp��6y   $
��Fill=�6�   $	��586��6�   $��10��6�   $��20��6�   $��587.800��6�    J� OpacityMask 5H      �� 6!   $! Resources/progressbar_1.bmp��6,   5I      �� 5J   
   -   5         ��Name$ labelTotalDW��5J      $��Center=�6&   $��	0,298,0,0q�6C   $	��Top=�6V   $��9(�6n   ���	#FFE6E6E6$
��100%��6�   ��6�   Z� 6�   �� 5L   
   -   5        $ TotalProgressTitle��5L      $��Total progress��6,   $��Center=�6E   $��Center=�6i   $��	0,271,0,0q�6�   $	��Top=�6�   
�������$��297.333��6�   5M      5N      