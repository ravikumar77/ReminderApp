   M S B A M L   `   `   `  ����F  BReminderApp, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:ReminderAppReminderApp  5   	       ReminderApp.MainWindow   5   	   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      5   	   R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:ReminderApp     5   	   -   5          � Title$  Reminder App��5   	   $	��450��6   $	��800��6+   .��5
      � $��10q�6    � RowDefinitions 5   
   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� $��	Reminders��6   $��24(�6%   $
��Bold"�63   $��0,0,0,10q�6E   &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ RemindersGrid��5      $��1��6*      AutoGenerateColumns$ False��67      CanUserAddRows$ False��5         SelectionMode$ Single=�6*   $��0,0,0,10q�6A      Columns 6`   0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Message��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$ 2* 6R   1  +System.Windows.Controls.DataGridBoundColumn	   Binding	 66   �� *	Message+6R     5      $ Date & Time��6%   $ * 6�   	 6>   �� *
DateTime+
 �� StringFormat$
 {0:MM/dd/yyyy HH:mm}��6�     5      $ Recurrence Type��6%   $ * 6a   	 6>   �� *RecurrenceType+6a     5      $ Repeat Interval��6%   $ * 6�   	 6>   �� *RepeatInterval+$
 {0:hh\:mm\:ss}��6�   4  .System.Windows.Controls.DataGridCheckBoxColumn  5      $ Weekdays Only��6)   $ 90 6a   	 6@   �� *WeekdaysOnly+6a     5      $ End Date��6%   $ * 6t   	 67   �� *	EndDate+$
 {0:MM/dd/yyyy}��6t     5      $ Active��6)   $ 60 6V   	 69   �� *
IsActive+6V   5      �� 5   
   $��2��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   .+�5       �� -   5         ��Name$ AddBtn��5       $	��Add��6%   $��80��63   $��0,0,10,0q�6>   6P   �� 5!      -   5        $ EditBtn��5!      $
��Edit��6&   $��80��65   $��0,0,10,0q�6@   6R   �� 5"      -   5        $ 	DeleteBtn��5"      $��Delete��6(   $��80��69   6D   5#      5$      5%      