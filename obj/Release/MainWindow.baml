   M S B A M L   `   `   `  ���� O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem   Valheim Online Launcher(   "Valheim_Online_Launcher.MainWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Aav2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     9system&clr-namespace:System;assembly=mscorlib       � Title$  Launcher��$	��420��$	��750�� � Cursor$ Arrow�� � 
ResizeMode$ NoResize=� � WindowStartupLocation$ CenterScreen=� � WindowStyle$
 None=� � AllowsTransparency .  J� Opacity$ 1Q� � Icon$ Resources/favicon.ico����Z�  � 	Resources    	ImageFill	 �� 
TargetType
 �� Property �� Value �� 
TargetType �Name J� SnapsToDevicePixels ��Name � Value �� CornerRadius��%4  &	        �� "	 �.��� 
 � ��$ 1q��� 
 � �� �� " �.R�� $ TemplateRoot�� . .+��� $ 
PART_Track��$$��Resources/test_progressbar.png��$��1q�$
��Fill=��� $*��$Resources/test_progressbar_green.png��$��1q�$
��Fill=�#��z $
��Left=�$	 0.2Q��� #��z��#��z��$ 2��.��� -   $��0,0,0,0q�����  �� ImageSource$ /Resources/image.png��.+��� -    ��Name$ button��$��0,6,8,0q�$	��Top=�$��11��$��0q�$��Right=�$��11�� J� IsHitTestVisible . $
 Hand������ " ��.R��� .+��� $ Close2��$��Resources/x_hover.png��$
��Fill=�$y�	Collapsed=��� $ Close1��$��Resources/x.png��$
��Fill=�$y�Visible=� �� Triggers P�  P� Property �@J�IsMouseOver P� Value . . ���  �� 
TargetName$ Close2��
 � y�$ Visible=��� $ Close1��
 � y�$ 	Collapsed=�� -   $��StartButtonGrid��.+��� -   $ StartGameBtn��$��Center=�$��0,0,0,7q�$��Bottom=�$	��160��$��51������ " ��.R��� .+��� $ Close2��$��Resources/start_hover.png��$
��Fill=�$y�	Collapsed=��� $ Close1��$��Resources/start.png��$
��Fill=�$y�Visible=� P�  �@J�IsMouseOver . . ��� $ Close2��
 � y�$ Visible=��� $ Close1��
 � y�$ 	Collapsed=��� -   $ 	FullCheck��$
��Left=�$��8,0,0,10q�$��Bottom=�$��32��$��32������ " ��.R��� .+��� $ Close2��$!��Resources/service_green.png��$
��Fill=�$y�	Collapsed=��� $ Close1��$��Resources/service.png��$
��Fill=�$y�Visible=� P�  �@J�IsMouseOver . . ��� $ Close2��
 � y�$ Visible=��� $ Close1��
 � y�$ 	Collapsed=�.���  � AllowsTransparency .  � VerticalOffset$ -5��$	��Top=����� *IsMouseOver+ �� ElementName$ 	FullCheck�� �� Mode$ OneWay=� � PlacementTarget �� $ 	FullCheck��.���� 
�������Полная проверка�� -   $ Discord��$
��Left=�$��	44,0,0,10q�$��Bottom=�$��32��$��32������ " ��.R��� .+��� $ Close2��$&�� Resources/discord_icon_green.png��$
��Fill=�$y�	Collapsed=��� $ Close1��$ ��Resources/discord_icon.png��$
��Fill=�$y�Visible=� P�  �@J�IsMouseOver . . ��� $ Close2��
 � y�$ Visible=��� $ Close1��
 � y�$ 	Collapsed=�.���  . $ -5��$	��Top=����� *IsMouseOver+$ Discord��$ OneWay=� �� $ Discord��.���� 
�������Дискорд�� -   $ Website��$
��Left=�$��	80,0,0,10q�$��Bottom=�$��32��$��32������ " ��.R��� .+��� $ Close2��$!��Resources/website_green.png��$
��Fill=�$y�	Collapsed=��� $ Close1��$��Resources/website.png��$
��Fill=�$y�Visible=� P�  �@J�IsMouseOver . . ��� $ Close2��
 � y�$ Visible=��� $ Close1��
 � y�$ 	Collapsed=�.���  . $ -5��$	��Top=����� *IsMouseOver+$ Website��$ OneWay=� �� $ Website��.���� 
�������
Сайт�� -   $ minimaze_btn��$��0,6,25,0q�$	��Top=�$��11��$��0q�$��Right=�$��11�� . $
 Hand�� J� RenderTransformOrigin$ 2.656,2.167&����� " ��.R��� .+��� $ Close2��$��Resources/minimaze.bmp��$
��Fill=�$y�	Collapsed=��� $ Close1��$"��Resources/minimaze_hover.bmp��$
��Fill=�$y�Visible=� P�  �@J�IsMouseOver . . ��� $ Close2��
 � y�$ Visible=��� $ Close1��
 � y�$ 	Collapsed=�� -	   $��NewsGrid��.+��� $��Resources/Paperblack.png��$	��255��$	��319��$
��Fill=�$��	0,41,13,0q�$	��Top=�$��Right=��� $#��Resources/Paperblackwhite.png��$	��251��$	��315��$
��Fill=�$��	0,43,15,0q�$	��Top=�$��Right=��� $
��Auto=�$��Disabled=�$��Right=�$	��250��$	��300��$��	0,50,17,0q�$	��Top=�.���� -
    ��Name$ 
ServerList�� .  $��5,0,0,0q�$	��Top=�$
��Left=�.+��� -   $
��News��$
��Wrap=�$��5,5,5,5q�$��Normal$�$��16(�
�������� -   $��ProgressBarGrid��$	��432��$��70��$��Bottom=�$
��Left=�$��	20,0,0,65q�.+�� -     �Name$  progressbar��$	 100Q�$��20��$��Bottom=�$��Stretch=�$��0,0,0,0q�#��[  ! � Maximum! �� *Width+$ ProgressBarGrid���� -   " ��Name$" totalpercent��$��Stretch=�$��Center=�$��0,0,0,-1q�$��Bottom=�$��9(����	#FFE6E6E6�� -   # ��Name$# currenttask��$��Stretch=�$��	0,0,0, 25q�$ �� TextAlignment$$ Center=�$��Bottom=�
�������$��16(�$��Oblique$�$��40��$
��Wrap=��� $��2q�
���i��