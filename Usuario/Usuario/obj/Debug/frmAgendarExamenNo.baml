   M S B A M L   `   `   `  ����B  >Usuario, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:UsuarioUsuario  5   	        Usuario.frmAgendarExamenNo   5   	   n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   !localclr-namespace:Usuario  5   	   -   5          � Title$  Agendar Exámen��5	      $	��750��6(   $
��1100��65    � 
ResizeMode$ NoResize=�6B    � WindowStartupLocation$ CenterScreen=�6X    � WindowStyle$
 None=�6}   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��33* �6   �� 5      $
��167* �6   .+�5   
   �� $��
0,60,126,0q�6   $��20q�6'   $��2��64   .��5      �� $��	-15,0,0,9q�6   $	��990��6,   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��800��66   $��
-170,8,8,8q�6B   .+�5      �� $ 
Horizontal=�6!   $	��400��6:   $��5,8,8,8q�6F   .+�5      �� -   5         ��Name$ txtCodigoPaciente��5   "   $
��0,10q�5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Código Paciente��5       %  !MaterialDesignFloatingHintTextBox#��[  5      $	��398��6X   $��14(�6d   {�.  6r   5      �� 5      $ 
Horizontal=�6!   $	��200��6:   $��5,8,8,8q�6F   .+�5      �� $��Tipo Examen��6   $��Center=�6"   $��0,0,10,0q�6=   
�������$��14(�6a   �� 5      -   5         ��Name$ cmbTipoExamen��5      $	��100��6   5      5      5      �� 5       $ 
Horizontal=�6   $	��800��66   $��
-170,8,8,8q�6B   .+�5!      �� $ 
Horizontal=�6!   $	��400��6:   $��5,8,8,8q�6F   .+�5"      �� -   5        $ txtNombrePaciente��5"   "   $
��0,10q�5#      $ Nombre Paciente��5$      #��[  5%      $	��398��6T   $��14(�6`   {�.  6n   5&      �� 5'      $ 
Horizontal=�6!   $	��800��6:   $��10,8,8,8q�6F   .+�5(      �� $��Especialidad��6    $��Center=�67   $��0,0,10,0q�6R   
�������$��14(�6v   �� 5)      -   5        $ cmbEspecialidad��5)   /   $	��100��6#   5*      5,      5-      �� 5.      $ 
Horizontal=�6   $	��800��66   $��
-170,8,8,8q�6B   .+�5/      �� $ 
Horizontal=�6!   $	��400��6:   $��5,8,8,8q�6F   .+�50      �� -   5        $ txtApellidoPaciente��50   "   $
��0,10q�51      $ Apellido Paciente��52      #��[  53      $	��398��6X   $��14(�6d   {�.  6r   54      �� 55      $ 
Horizontal=�6!   $	��400��6:   $��5,8,8,8q�6F   .+�56      �� $��Fecha Examen��6    $��Center=�67   $��0,0,10,0q�6R   
�������$��14(�6v   (  "System.Windows.Controls.DatePicker  57      -   5        $��dtFechaExamen��57   1   $	��277��6%   61   58      59      �� 5;      $
��0,10q�6   .+�5<      �� -   5        $ 	txtBuscar��5<      $ Buscar Codigo Paciente��5=      #��[  5>      $��14(�6@   6N   5?      � 5A      $
��0,20q�6   .+�5B      &   System.Windows.Controls.DataGrid  -	   5        	  Name$	 dgPacientes��5B      
   AutoGenerateColumns
 .     
IsReadOnly . $
��Auto=�5D         Columns 5E      0  *System.Windows.Controls.DataGridTextColumn  5F      ,  &System.Windows.Controls.DataGridColumn   Header$ Paciente ID��61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6F   �� *
PacienteID+  5G      $ Nombre Paciente��61    6J   �� *NombrePaciente+  5H      $ Apellido Paciente��61    6L   �� *ApellidoPaciente+5J      5K      5N      5P      �� 5Q   
   $��2��6   $��	0,3,0,638q�6(   ��5R      �� 5S       �� 
StartPoint$ 0.5,0&�6&    �� EndPoint$ 0.5,1&�69   .��5T      �  � Color$ #F0F3FA��6#   � 5U      $ #D5DEEF��6#    � Offset$ 1Q�63   � 5V      $ #B1C9EF��6#   $	 0.5Q�63   5W      5Y      �� 5Z   
   $ 
Horizontal=�6   $
��Left=�6.   $
��1000��6I   $��2��6V   $��
0,-710,0,0q�6j   .+�5[      �� -
   5        $��btnPacientes��5[      $��	Pacientes��6*   ���#395886
�������6g   �� 5\      -   5        $��btnHistorialesMedicos��5\      $��Historiales Médicos��62   ���#395886
�������6y   �� 5]      -   5        $��
btnMedicos��5]      $��Médicos��6'   ���#395886
�������6b   �� 5^      -   5        $��btnConsultas��5^      $��	Consultas��6)   ���#395886
�������6f   �� 5_      -   5        $��
btnRecetas��5_      $��Recetas��6'   ���#395886
�������6b   �� 5`      -   5        $��btnCitas��5`      $��Citas��6%   ���#395886
�������6^   �� 5a      -   5        $��btnExamenes��5a      $��	Exámenes��6(   ���#D5DEEF
�������$
��Bold"�6d   6v   �� 5b      -   5        $��btnReportes��5b      $��Reportes��6(   ���#395886
�������6P   �� 5c      -   5         ��Name$ btnUsuarios��5c      $��Usuarios��6*   ���#395886
�������6f   5e      �� 5f   
   $��Agendar Exámenes��6   $
��Left=�5g      $	��Top=�5h      $��40(�5i      $��	10,35,0,0q�5j      
���   �$��2��5k      � 5l   
   .+�5m      �� $��0,219,-984,0q�6   $	��522��6/   $	��Top=�6<   $��Right=�6T   $	��234��6p   .+�5n      �� -   5        $ 
btnNewUser��5n      $��50��6-   $��90��69   
�������
������� � ToolTip$ Nuevo usuario��6k   .��5o      �� $��Assets/AgregarDoc.png��6   $��60��6;   5p      �� 5q      -   5        $ 
btnGuardar��5q      $��50��6-   $��90��69   
�������
�������$ Guardar��6k   .��5r      �� $��Assets/save.png��6   $��60��65   5s      �� 5t      -   5        $ btnCancelar��5t      $��50��6.   $��90��6:   
�������
�������$ Cancelar��6l   .��5u      �� $��Assets/actualizar.png��6   $��60��6;   5v      5w      5x      � 5y   
   .+�5z      �� -   5        $ btnHome��5z      $��50��6&   
�������$��974,9,-906,691q�6E   
�������.��5{      �� $��Assets/regresar.png��6   $��50��65   $��60��6A   5|      5}      �� 5~   
   -   5        $ btnRegresar��5~      $��Regresar��6&   $��50��69   ���#395886$��798,625,0,75q�6Z   
�������$��1��6�   $��20(�6�   $
��Bold"�6�   6�   5      5�      