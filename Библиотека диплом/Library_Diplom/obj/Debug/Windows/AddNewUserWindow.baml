   M S B A M L   `   `   `  ����B  >Library, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null1clr-namespace:Library.WindowsLibrary.Windows  5   	   &     Library.Windows.AddNewUserWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   /localclr-namespace:Library.Windows     5   	     � Title$  Пользователь��5   	   $	��320��6   $	��300��6+    � 
ResizeMode$ CanMinimize=�67    � WindowStartupLocation$ CenterScreen=�6P   .��5	      �  � RowDefinitions 5
   
   �� 5      $��35 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��50 �6   �� 5      $��70 �6   .+�5   
   �� -   5         ��Name$ titleW��5      $��0,0,0,0q�6$   $
��Wrap=�65   $@��:Добавление нового пользователя��6I   $��Center=�6o   $��Center=�6�   $��0��6�   �� 5   
   $
��Wrap=�6   $%��ФИО пользователя��6(   $��0,5,0,0q�6@   $	��Top=�6Q   $��Center=�6i   $��1��6�   �� 5   
   -   5         ��Name$ nameU��5      $	��200��6!   $��0,0,0,5q�6G   $��Bottom=�6X   $��Center=�6s   $��1��6�   ��6-   �� *
FullName+6�   �� 5   
   $
��Wrap=�6   $��
Логин��6(   $��0,5,0,0q�65   $	��Top=�6F   $��Center=�6^   $��2��6{   �� 5   
   -   5        $ loginU��5      $	��200��6"   $��0,0,0,5q�6E   $��Bottom=�6V   $��Center=�6q   $��2��6�   ��6.   �� *Login+6�   �� 5   
   $
��Wrap=�6   $��Пароль��6(   $��0,5,0,0q�66   $	��Top=�6G   $��Center=�6_   $��3��6|   �� 5   
   -   5        $ passU��5      $	��200��6!   $��0,0,0,5q�6G   $��Bottom=�6X   $��Center=�6s   $��3��6�   ��6-   �� *
Password+6�   �� 5   
   $
��Wrap=�6   $��Роль��6(   $��0,5,0,0q�64   $	��Top=�6E   $��Center=�6]   $��4��6z   �� 5   
   -   5         ��Name$ roleU��5      $	��200��6"    �� DisplayMemberPath$ Title��6L   $��0,0,0,5q�6f   $��Bottom=�6w   $��Center=�6�   $��4��6�    �� SelectedItem 6.   �� *Role+6�   �� 5!   
   -   5        	 ��Name$	 addBTN��5!      $��Добавить��6!   $��0,0,0,25q�6I   $��Center=�6[   $��Center=�6v   $��5��6�   5"      5#      