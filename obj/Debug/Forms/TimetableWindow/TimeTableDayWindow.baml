   M S B A M L   `   `   `  ����H  DChanceryStore, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY1clr-namespace:ChanceryStore.Forms.TimetableWindow#ChanceryStore.Forms.TimetableWindow  5   	   <    6ChanceryStore.Forms.TimetableWindow.TimeTableDayWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   Clocal1clr-namespace:ChanceryStore.Forms.TimetableWindow     5   	     � Title$6  0Понедельник - день тяжелый��5   	   $	��432��6,   $	��758��69    � WindowStartupLocation$ CenterOwner=�6E   .��5	      � -   5         �Name$6 0Понедельник___день_тяжелый��5	       � ColumnDefinitions 5
   
   �� 5      .+�5   
   �� -   5         ��Name$ employmentBtn��5         btnStyle#���   6(     btnTemplate2#���  6K   $)��#отметить занятость��6v   $
��Left=�6�   $��
499,20,0,0q�6�   $	��Top=�6�   $	��129��6�   $��24��6�   $��12(�6  �� 5   
   -   5         ��Name$ lvMonday��5      $
��Left=�6%   $	��271��6@   $��	50,97,0,0q�6M   $	��Top=�6`   $	��652��6x   $��Century Gothic)�6�   ���	#FF535353$
��Bold"�6�   $��Hidden=�6�   ��6�   Z�  �� View 5      �� 5      .P�5      �� $��
Время��6&    �� CellTemplate 5      �� 5      .R�5   "   ��  �� Orientation$ 
Horizontal=�6.   .+�5   &   �� $��Century Gothic)�60   $	��с ��6M   $��11(�6W   �� 5   &   $��Century Gothic)�60   $��14(�6l   ��6M   �� *TimeStartView+6l   �� 5   &   $��Century Gothic)�60   $�� до ��6L   $��11(�6X   �� 5   &   $��Century Gothic)�60   $��14(�6j   ��6M   �� *TimeEndView+6j   5   #   5      5!      �� 5$      $��Работник��6&    5%      �� 5&      .R�5'   "   �� $	��500��6T   ��6+   �� *UsersOneTimeView+��5(   &   �� 5)   *   .R�5*   .   �� $ 
Horizontal=�69   .+�5+   2   A� $��30��6:   $��30��6E   $��5q�6Q   ��5,   6   �� 5-   :   	 j� Stretch$	 UniformToFill=�6c   
 �� ImageSource
 6E   �� *Image+6c   5/   3   �� 50   2   $��Century Gothic)�6<   $��11(�6Y   $��9,0,9,0q�6g   $��ФИО��6x   ���	#FFB4B4B4$��Center=�6�   $��Center=�6�   �� 51   2   $��Century Gothic)�6<   $��12(�6Y   $��9,0,4,0q�6g   ���	#FF535353$��Center=�6�   $��Center=�6�   ��6x   �� *	FirstName+6�   �� 52   2   $��Century Gothic)�6<   $��12(�6X   $��4,0,9,0q�6f   ���	#FF535353$��Center=�6�   $��Center=�6�   ��6w   �� *
LastName+6�   �� 53   2   $��Century Gothic)�6<   $��11(�6X   $��9,0,9,0q�6f   $�� должность ��6w   ���	#FFB4B4B4$��Center=�6�   $��Center=�6�   �� 54   2   $��Century Gothic)�6<   $��12(�6X   $��9,0,9,0q�6f   ���	#FF535353$��Center=�6�   $��Center=�6�   ��6w   �� *	Type.Name+6�   55   /   57   +   59   #   5;      5=      5?      5A      �� 5B   
   -   5        $ AddProductBtn��5B      $��131,21,535.333,351.667q�6(   #���   6H   #���  6l   $��12(�6�   $��+ добавить��6�   $��85��6�   $��30��6�   �� 5C   
   -   5        $ RemoveTimetableBtn��5C      $��226,21,437.333,351.667q�6-   #���   6N   #���  6q   $��12(�6�   $��🗑️ удалить��6�   $��88��6�   $��30��6�   �� 5D   
   -   5        $ EditProductBtn��5D      $��32,21,631.333,351.667q�6)   #���   6H   #���  6l   $��12(�6�   $��✏️ изменить��6�   
 � Tag$
 Edit��6�   $��88��6�   $��30��6�   �� 5E   
   $
��Left=�6   $��
309,65,0,0q�6/   $
��Wrap=�6C   $��Понедельник��6W   $	��Top=�6j   $��Century Gothic)�6�   $��20(�6�   5G      5H      