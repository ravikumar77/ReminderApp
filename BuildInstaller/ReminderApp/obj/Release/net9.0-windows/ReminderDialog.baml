   M S B A M L   `   `   `  ����   ReminderApp     ReminderApp.ReminderDialog   W SPresentationCore, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=9.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35H 9http://schemas.microsoft.com/winfx/2006/xaml/presentation      R NSystem.Xaml, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  Reminder��$	��450��$	��500�� � WindowStartupLocation$ CenterOwner=� � 
ResizeMode$ NoResize=�.��� $��10q� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $��* �.+��� $��Message:��$��0��$��0��$��Center=��� -    ��Name$ 
MessageBox��$��0��$��1��$��5q�$��24���� $��Date:��$��1��$��0��$��Center=�$��0,10,0,0q�(  "System.Windows.Controls.DatePicker  -     Name$ 
DatePicker��$��1��$��1��$��5,10,5,5q��� $��Time:��$��2��$��0��$��Center=�� $��2��$��1��$��5q� �� $��50 ��� $
��Auto ��� $��50 ��� $
��Auto ��� $��80 ��� $��* �.+��� -    ��Name$ 
HourPicker��$��0��$��24��.@��� $��12���� $��1���� $��2���� $��3���� $��4���� $��5���� $��6���� $��7���� $��8���� $��9���� $��10���� $��11���� $��:��$��1��$
��Bold"�$��Center=�$��Center=�$	��2,0q��� -   $ MinutePicker��$��2��$��24��$��60���� -   $ 
AmPmPicker��$��4��$��60��$
��Left=�$��10,0,0,0q�$��24��.@��� $��AM���� $��PM���� $��Recurrence:��$��3��$��0��$��Center=��� -   $ RecurrenceTypeCombo��$��3��$��1��$��5q�$��24��.@��� $
��None��
 � Tag$
 None���� $��Daily��$ Daily���� $��Weekdays Only��$ Weekdays���� $��Custom��$ Custom���� $ ��Repeat Interval (minutes):��$��4��$��0��$��Center=�� $��4��$��1��$��5q� �� $��* ��� $
��Auto �.+��� -   $ IntervalBox��$��0��$��24���� $��(0 = no repeat)��$��1��$��Center=�$��5,0,0,0q�
��������� -   	 ��Name$	 WeekdaysOnlyCheckBox��$��5��$��1��$��Skip weekends��$��5q�$��Center=��� $��	End Date:��$��6��$��0��$��Center=�� $��6��$��1��$��5q� �� $
��Auto ��� $��* �.+��� -	   $	 HasEndDateCheckBox��$��Set end date��$��Center=�  -
   $ EndDatePicker��$��1��$��10,0,0,0q�${�False���� $��9��$��0��$��2��
 �� Orientation$
 
Horizontal=�$��Right=�$��0,20,0,0q�.+��� -   $��OK��$��80��$��0,0,10,0q��� -   $��Cancel��$��80��