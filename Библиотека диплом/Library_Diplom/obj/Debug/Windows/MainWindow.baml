   M S B A M L   `   `   `  ����B  >Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:LibraryLibrary  5   	       Library.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Library     5   	   -   5        $
��home��5        � Title$  
MainWindow��5   	   $	��650��6   $
��1000��6)    � WindowStartupLocation$ CenterScreen=�5	   	    � WindowStyle$
 None=�6.   
������  � AllowsTransparency .  � 
ResizeMode$ NoResize=�5   	    � WindowState$ Normal=�6   .��5      ��  �� CornerRadius$ 8��6      PrimaryBackgroundColor#���   6   ._�5   
   �  � ColumnDefinitions 5      �� 5      $
��Auto �6#   �� 5      .+�5      �� $ 8 0 0 8��6     SecundaryBackgroundColor#���  6,   ._�5      � -   5         �Name$ gridMenu��5      $��60��6)   	 � RowDefinitions	 5      �� 5      $��60 �6(   �� 5      $��80 �6(   �� 5      $
��Auto �6(   �� 5      .+�5      �� $��1��6!   
 �� Orientation$
 
Horizontal=�6.   $	��Top=�6G   .+�5      �� $��/Resources/logoApp.png��6    $��7 0 0 0q�6@   $��45��6Q   $��45��6\   $
��Left=�6h   �� 5      $
��Bibl��6      PrimaryBlueColor#���  6/   $
��Bold"�6_   $��20(�6q   $��Center=�6   $
��Left=�6�   $
��Left=�6�   $��14 0 0 0q�6�   5       d� 5#      -   5         d�Name$ btnMenu��5#   #   $��Right=�65   $��0q�6Q   
������ $��Center=�6~     MenuIconStyle#��[ 6�   $��0 0 15 0q�6�    � Triggers 5$      :� 5%       :� RoutedEvent$ ToggleButton.Unchecked��6+   .[�5&   "   �� .��5'   &   �� -   5         ��Name$ HideStackPanel��5'   1   .��5(   *   W�  �� 
TargetName$ gridMenu��6:    �� TargetProperty$ Width�6[    g� 	BeginTime$ 0:0:0X�6}    W� From$	 200X�6�   	 W� To$ 60X�6�    g� Duration$ 0:0:0.3D�6�   5)   +   5*   '   5+   #   5,      :� 5-      $ ToggleButton.Checked��6+   .[�5.   "   �� .��5/   &   �� -   5        $ ShowStackPanel��5/   1   .��50   *   W� $ gridMenu��6:   $ Width�6[   $ 0:0:0X�6}   $ 60X�6�   $	 200X�6�   $ 0:0:0.3D�6�   51   +   52   '   53   #   54      56      �� 58      $��3��6!   .+�59      	� -   5         	�Name$ 	rdClients��59   &   $��Клиенты��69     MenuRadioButtonStyle#���  6K     home
 � Tag# �  6z   6�   	� 5:      -   5        $ rdBooks��5:   &   $��
Книги��67   #���  6G     
headphones# �  6v   $��0 5 0 5q�6�   6�   	� 5;      -   5        $ rdRent��5;   &    d� 	IsChecked$
 TrueY�66   $��Аренда��6G   #���  6X    	 notes# �  6�   6�   	� 5<      -	   5        $ rdExit��5<   &   $��
Выход��66   #���  6F     payment# �  6u   $��0 5 0 5q�6�   6�   5=      5>      5?      � 5C      $��1��6   	 5D      �� 5E      $��60 �6$   �� 5F      .+�5I      �� -
   5        $��0��5I      #���   6&   $��0,0,10,0q�6y   �� 5L      $��0��6   $��4��6*   $��Right=�6:   $	��Top=�6V   $��0,5,10,0q�6n   $��30��6�   $
 
Horizontal=�6�   $��RightToLeft=�6�   .+�5M      �� -   5         ��Name$ btnClose��5M       	 IconButtonsStyle#��[	 6/    	
 close#��� 
 6Y   $ IsCloseButton��6�   �� 5N      -   5        $ 
btnRestore��5N      $	��5,0q�61   ��5O      �� 5P       �� 
TargetType" ��6$    �� BasedOn# [	 6A   .�5Q   "   ��  �� Property � ��  maximize �� Value# �  6<    �� Triggers 5R   "   �� 5S   &    �� Binding 62   ��  �� ElementName$
 home��  �� Path$  WindowState�! �� Value! 	Maximized6i   .a�5T   *   ��  � ��  restore# �  6D   5U   '   5W      5Y      �� 5Z      -   5        $ btnMinimize��5Z      #��� 	 62     minimize#���  6]   6�   5[      �� 5]      $��1��6   $��10q�6*   ._�5^       � -   5        "  �Name$" 	MainFrame��5^      #  � NavigationUIVisibility$# Hidden=�6/   5`      5a      5b      5d      5e      5f      