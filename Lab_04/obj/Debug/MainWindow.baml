   M S B A M L   `   `   `  ����A  =Lab_04, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:Lab_04Lab_04  5   	       Lab_04.MainWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   &localclr-namespace:Lab_04     5   	     � Title$  Database Terminal��5   	   $	��450��6#   $	��800��60    � 
ResizeMode$ NoResize=�6<   .��5	      � ���#B2E153.+�5
   
   �� $
��Home��6   
�������$��72(�62   $��	25,25,0,0q�6@   $
��Left=�6S   $	��Top=�6n   �� 5   
   -   5         ��Name$ musicShowBtn��5      $	��250��6'   $��50��63   $��	0,0,350,0q�6?   ���	#FF74A80C � Cursor$
 Hand��6�    �� CornerRadius$ 10,10,10,10��6�   ._�5      �� $��Music ��6   $��Center=�6%   $��Center=�6B   $��18(�6]   
���������6~   Z� 5      �� 5   
   -   5        $ plateShowBtn��5      $	��250��6'   $��50��63   $��	350,0,0,0q�6?   ���	#FF74A80C$
 Hand��6�   $ 10,10,10,10��6�   ._�5      �� $��Plate��6   $��18(�6$   $��Center=�62   $��Center=�6O   
�������6j   5      �� 5   
   -   5        $ musicianShowBtn��5      $	��250��6*   $��50��66   $��Center=�6B   $��Center=�6_   $��	0,150,0,0q�6z   ���	#FF74A80C$
 Hand��6�   $ 10,10,10,10��6�   ._�5      �� $��Musician��6   $��Center=�6'   $��Center=�6D   $��18(�6_   
�������6m   5      � 5   
   -   5         �Name$ tableWindow��5      ���#B2E153$y�Hidden=�69   .+�5      �� $
��Left=�6   $��Center=�60   $	��550��6K   $	��400��6W   $��10,0,0,0q�6d   ���	#FF97DB0F$ 5,5,5,5��6�   ._�5      &   System.Windows.Controls.DataGrid  -   5          Name$ mainDataGrid��5      $��16(�61   $ �� IsSynchronizedWithCurrentItem$ FalseY�6?   $��Center=�6e   $��Center=�6�      CanUserReorderColumns .  
�������
���   �
�������$��5,5,5,5q�6  ��. 	   HorizontalGridLinesBrush
	 �   �
   VerticalGridLinesBrush

 �   �6F  5      �� 5      $��Settings��6   $��Right=�6'   $	��Top=�6C   $��	0,10,41,0q�6[   $��36(�6n   
�������6|   �� 5      $��Table type:��6   $��Right=�6*   $��Center=�6F   $��0,0,120,275q�6a   $��18(�6v   
�������6�   �� 5      -   5         ��Name$ tableTypeList��5      $��50��6.   $	��200��6:   $��Right=�6F   $��18(�6b   $��
0,0,10,200q�6p    �� SelectedIndex$ 0��6�   $��Center=�6�   $��Center=�6�   ��6�   Z� ��6�   Z� 6�   �� 5      $��Filter type:��6   $��Right=�6+   $��Center=�6G   $��
0,0,120,75q�6b   $��18(�6v   
�������6�   �� 5      -   5        $ tableFilterTypeList��5      $��50��64   $	��200��6@   $��Right=�6L   $��18(�6h   $��0,0,10,0q�6v   $ 0��6�   $��Center=�6�   $��Center=�6�   �� 5      $��Name of an entity��6   $��Right=�60   $��Center=�6L   $��0,0,63,-100q�6g   $��18(�6|   
�������6�   �� 5      -   5         ��Name$ 
nameFilter��5      $��50��6*   $	��200��66   $��Right=�6B   $��18(�6^   $��0,0,10,-175q�6l   $��Center=�6�   $��Center=�6�   ���	#FFEAEAEA
���   �$��Normal$�6
  �� 5      -	   5        $	��200��5      $��50��6!   $��Right=�6-   $��Center=�6I   $��
0,300,10,0q�6d   ���	#FF74A80C$ 10,10,10,10��6�   ._�5       �� $
��Back��6   $��Center=�6'   $��Center=�6D   $��18(�6_   
�������6m   5!      5"      5#      5$      