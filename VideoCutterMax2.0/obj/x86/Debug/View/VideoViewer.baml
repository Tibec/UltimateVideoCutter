   M S B A M L   `   `   `  ����J  FVideoCutterMax2, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null;"clr-namespace:VideoCutterMax2.ViewVideoCutterMax2.View  5      &     VideoCutterMax2.View.VideoViewer   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      4local"clr-namespace:VideoCutterMax2.View     5        � DataContext  5
      E'clr-namespace:VideoCutterMax2.ViewModelVideoCutterMax2.ViewModel  5   "   4  .VideoCutterMax2.ViewModel.VideoViewerViewModel  6
   6vm'clr-namespace:VideoCutterMax2.ViewModel     6"   .��5      � .+�5   
   r� -   5         r�Name$ 	mainVideo��5      $��0q�6*    r� LoadedBehavior$
 Stop=�65    r� Source 6K   �� *CurrentVideo.Uri+6o   � 5   
   $��Bottom=�6    � ColumnDefinitions 5      �� 5      $
��auto �6#   �� 5      �� 5      $
��auto �6#   �� 5      $
��auto �6#   �� 5      $
��auto �6#   .+�5      �� -   5         ��Name$ 	videoPlay��5      $
��Play��6(   $
��Left=�6[   $��Bottom=�6v   $��50��6�   {�67   �� *IsVideoLoaded+6�   �� 5      -   5         ��Name$ videoSlider��5      $	��Top=�6g   $��1��6    �� TickFrequency$ 1Q�6�   6�   �� 5      -   5         ��Name$ videoTextTimer��5      $��00.00.00 / 00.00.00��60   
�������$
��auto��6^   $
��Wrap=�6l   $��Center=�6�   $��2��6�   $��0q�6�   $��0��6�   	 �� Padding$
	 10,4q�6�   $
��Left=�6�   $��8(�6�   �� 5       -   5        $ videoBeginButton��5       $��Begin��6Q   $��Right=�6a   $	��Top=�6}   $��50��6�   $��3��6�   {�6/   �� *BeginEnable+��6�   �� *AddBeginCommand+�� 5!      -   5        $ videoEndButton��5!      $	��End��6M   $��Right=�6[   $	��Top=�6w   $��50��6�   $��4��6�   {�6-   �� *	EndEnable+��6�   �� *AddEndCommand+5$      5&      5'      