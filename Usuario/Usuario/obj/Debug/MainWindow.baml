   M S B A M L   `   `   `  ����B  >Usuario, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:UsuarioUsuario  5   	       Usuario.MainWindow   5   	   n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   !localclr-namespace:Usuario  5   	     � Title$  Identificacion de Usuario��5	      $	��500��63   $	��700��6@    � 
ResizeMode$ NoResize=�6L    � WindowStartupLocation$ CenterScreen=�6b    � WindowStyle$
 None=�6�   .��5
      �  � Cursor$  ��6   .+�5   
   �� $	��100��6   $	��Top=�6!   $��491,16,119,0q�69    �� RadiusX$ 20��6O    �� RadiusY$ 20��6\   ��5      �� 5       �� 
StartPoint$ 0.5,0&�6&    �� EndPoint$ 0.5,1&�69   .��5      � 	 � Color$	 #B1C9EF��6#   � 5      $	 #8AAEE0��6#   
 � Offset$
 1Q�63   � 5      $	 #628ECB��6#   $	
 0.5Q�63   5      5      �� 5   
   $	��500��6   $��Center=�6!   $��-50,0,350,0q�6<   $ 50��6Q   $ 50��6^   ��5      �� 5      $ 0.5,0&�6&   $ 0.5,1&�69   .��5      � $	 #F0F3FA��6#   � 5      $	 #D5DEEF��6#   $
 1Q�63   � 5      $	 #B1C9EF��6#   $	
 0.5Q�63   5      5      �� 5   
   $	��280��6   $	��240��6    
�������$	��Top=�6:   $��397,160,23,0q�6R   $ 10��6h   $ 10��6u    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5         
BlurRadius$ 15Q�6#      Color$ 	#FFBBBBBB��63      	Direction$ 0Q�6E      RenderingBias$ Quality=�6S   5"      � 5#   
   $	��280��6   $	��240��6   $��396,160,24,0q�6(   $	��Top=�6>   .+�5$      �� $��LOGIN��6   $��Center=�6$   $	��Top=�6A   $��5q�6Y   
�������$��18(�6v   �� 5%      $��Center=�6   $��15q�64   .+�5&      �� -   5         ��Name$ 	txtCorreo��5&      $��0, 10q�5'      $��18(�5(      $��Corbel Light)�5)      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Correo��5*       %  !MaterialDesignFloatingHintTextBox#��[  5+      G� 5,      -   5         G�Name$ txtPassword��5,      $��0, 10q�5-      $��18(�5.      $��Corbel Light)�5/      $ Contraseña��50       ) %MaterialDesignFloatingHintPasswordBox#��[ 51      52      53      �� 54   
   -   5         ��Name$ btnIngresar��54      $��Ingresar��6&   $	��150��69   $
��Left=�6E   $	��Top=�6`   $��461,378,0,0q�6x   ���#3958866�   �� 55   
   $��HOSPITAL PLUS��6   $
��Left=�6(   $��
58,378,0,0q�6C   $	��Top=�6W   $��48(�6o   $��Gabriola)�6}   �� 56   
   $	��353��6   $��-190,57,164,0q�6   $��/Assets/fondo.png��64   $	��Top=�6O   �� 57   
   $	��100��6   $��491,20,119,0q�6   $��90��63   $��/Assets/home.png��6>   $	��Top=�6X   58      59      