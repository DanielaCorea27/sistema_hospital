   M S B A M L   `   `   `  ����B  >Usuario, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:UsuarioUsuario  5   	       Usuario.frmUsuarios   5   	   n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   !localclr-namespace:Usuario  5   	   -   5          � Title$  Usuarios��5	      $	��750��6   $
��1100��6+    � 
ResizeMode$ NoResize=�68    � WindowStartupLocation$ CenterScreen=�6N    � WindowStyle$
 None=�6s   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��33* �6   �� 5      $
��167* �6   .+�5   
   �� $��
0,60,126,0q�6   $��20q�6'   $��2��64   .��5      �� .+�5      �� -   5         ��Name$ 	txtNombre��5      $
��0,10q�5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Nombre��5       %  !MaterialDesignFloatingHintTextBox#��[  5      �� 5      -   5        $ txtApellido��5      $
��0,10q�5      $ Apellido��5      #��[  5      �� 5      -   5        $ txtEmail��5      $
��0,10q�5      $ Email��5      #��[  5      �� 5       �� Orientation$ 
Horizontal=�6   $	��664��66   $
��Left=�6B   .+�5       �� $��Roles��6   $��Center=�6(   $��0,0,10,0q�6C   
�������6U   �� 5!      -   5         ��Name$ cmbRoles��5!   '   $	��100��6   5"      5#      G� 5$      -   5        	 G�Name$	 txtClave��5$      $
��0,10q�5%      $ Contraseña��5&       # MaterialDesignFilledPasswordBox#��[ 5'      G� 5(      -   5        $	 txtConfirmacion��5(      $
��0,10q�5)      $ Confirmar Contraseña��5*      #��[ 5+      �� 5,      $
��0,10q�6   .+�5-      �� -   5        $ 	txtBuscar��5-      $ Buscar Codigo��5.      #��[  50      51      �� 53      $
��Auto=�6    $��0,0,0,0q�6C   $��1��6T   .��54      &   System.Windows.Controls.DataGrid  -	   5        
  Name$
 
dgUsuarios��54         AutoGenerateColumns .     
IsReadOnly .    Columns 55      0  *System.Windows.Controls.DataGridTextColumn  56      ,  &System.Windows.Controls.DataGridColumn   Header$ Codigo��61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6A   �� *	usuarioId+6a     57      $ Nombre��61    6A   �� *Nombre+6^     58      $ Apellido��61    6C   �� *
Apellido+6b     59      $ Email��61    6@   �� *Email+6\     5:      $	 Rol��61    6>   �� *Role+6Y     5;      $ Clave��61      
Visibility$ Hidden=�6Z    6@   �� *Clave+6p   5=      5>      5@      5B      �� 5C   
   $��2��6   $��	0,3,0,638q�6(   ��5D      �� 5E       �� 
StartPoint$ 0.5,0&�6&    �� EndPoint$ 0.5,1&�69   .��5F      �  � Color$ #F0F3FA��6#   � 5G      $ #D5DEEF��6#    � Offset$ 1Q�63   � 5H      $ #B1C9EF��6#   $	 0.5Q�63   5I      5K      �� 5L   
   $ 
Horizontal=�6   $
��Left=�6.   $
��1000��6I   $��2��6V   $��
0,-710,0,0q�6j   .+�5M      �� -
   5        $��btnPacientes��5M      $��	Pacientes��6)   ���#395886
�������6f   �� 5N      -   5        $��btnHistorialesMedicos��5N      $��Historiales Médicos��62   ���#395886
�������6y   �� 5O      -   5         ��Name$ 
btnMedicos��5O      $��Médicos��6)   ���#395886
�������6d   �� 5P      -   5        $��btnConsultas��5P      $��	Consultas��6)   ���#395886
�������6f   �� 5Q      -   5        $��
btnRecetas��5Q      $��Recetas��6'   ���#395886
�������6b   �� 5R      -   5        $��btnCitas��5R      $��Citas��6%   ���#395886
�������6^   �� 5S      -   5        $��btnExamenes��5S      $��Examenes��6(   ���#395886
�������6d   �� 5T      -   5        $��btnReportes��5T      $��Reportes��6(   ���#395886
�������6P   �� 5U      -   5        $��btnUsuarios��5U      $��Usuarios��6(   ���#D5DEEF
�������$
��Bold"�6d   5V      �� 5W   
   $��Registro de Usuarios��6   $
��Left=�5X      $	��Top=�5Y      $��40(�5Z      $��	10,35,0,0q�5[      
���   �$��2��5\      � 5]   
   .+�5^      �� $��0,206,-974,0q�6   $	��400��6/   $	��Top=�6<   $��Right=�6T   $	��234��6p   .+�5_      �� -   5        $ 
btnNewUser��5_      $��50��6-   $��90��69   
�������
������� � ToolTip$ Nuevo usuario��6k   .��5`      �� $��Assets/newuser.png��6   $��60��68   5a      �� 5b      -   5        $ 
btnGuardar��5b      $��50��6-   $��90��69   
�������
�������$ Guardar��6k   .��5c      �� $��Assets/save.png��6   $��60��65   5d      �� 5e      -   5        $ 	btnEditar��5e      $��50��6,   $��90��68   
�������
�������$ Editar��6j   .��5f      �� $��Assets/edit.png��6   $��60��65   5g      �� 5h      -   5        $ btnEliminar��5h      $��50��6.   $��90��6:   
�������
�������$ Eliminar��6l   .��5i      �� $��Assets/delete.png��6   $��60��67   5j      �� 5k      -   5        $ btnCancelar��5k      $��50��6.   $��90��6:   
�������
�������$ Cancelar��6l   .��5l      �� $��Assets/actualizar.png��6   $��60��6;   5m      5n      5o      � 5p   
   .+�5q      �� -   5        $ btnHome��5q      $��50��6&   
�������$��979,5,-912,695q�6E   
�������.��5r      �� $��Assets/regresar.png��6   $��50��65   $��60��6A   5s      5t      5v      5w      