   M S B A M L   `   `   `  ����B  >Usuario, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:UsuarioUsuario  5   	        Usuario.frmRegistroRecetas   5   	   n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   !localclr-namespace:Usuario  5   	     � Title$  Gestión de Recetas��5	      $	��750��6,   $
��1100��69    � 
ResizeMode$ NoResize=�6F    � WindowStartupLocation$ CenterScreen=�6\    � WindowStyle$
 None=�6�    � Cursor$  ��6�   .��5      �  � ColumnDefinitions 5   
   �� 5      $	��33* �6   �� 5      $
��167* �6   .+�5   
   �� $��-190,60,126,0q�6   $��20q�6*   $��2��67   $	��800��6K   .��5      �� $��-15,0,0,-19q�6   $	��990��6.   .+�5      ��  �� Orientation$ 
Horizontal=�6   $	��800��66   $��
-170,0,8,8q�6B   .+�5      �� $ 
Horizontal=�6!   $	��400��6:   $��5,8,8,8q�6F   .+�5      �� -   5         ��Name$ txtCodigoPaciente��5   "   $
��0,10q�5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Carnet Paciente��5       %  !MaterialDesignFloatingHintTextBox#��[  5      $	��398��6H   $��16(�6T   {�.  6b   5      �� 5      $ 
Horizontal=�6!   $	��400��6:   $��5,8,8,8q�6F   .+�5      �� -   5        $ txtCodigoMedico��5   "   $
��0,10q�5      $ Carnet Médico��5      #��[  5      $	��398��6H   $��16(�6T   {�.  6b   5      5       �� 5"      $ 
Horizontal=�6   $	��800��66   $��-170,-10,8,8q�6B   .+�5#      �� $ 
Horizontal=�6!   $	��400��6:   $��5,8,8,8q�6F   .+�5$      �� -   5        $ txtConsulta��5$   "   $
��0,10q�5%      $ Código Consulta��5&      #��[  5'      $	��398��6H   $��16(�6T   {�.  6b   5(      �� 5)      $ 
Horizontal=�6!   $	��400��6:   $��5,8,8,8q�6F   .+�5*      �� $��Fecha Emision��6    $��Center=�68   $��0,0,10,0q�6S   
�������$��16(�6w   {�.  6�   (  "System.Windows.Controls.DatePicker  5+      -   5        $��dtFechaReceta��5+   1   $	��247��6%   	   IsTodayHighlighted	 .  {�.  $��14(�6s   5,      5-      �� 5.      $ 
Horizontal=�6   $	��800��66   $��-170,-20,8,8q�6B   .+�5/      �� $ 
Horizontal=�6!   $	��400��6:   $��5,8,8,8q�6F   .+�50      �� -   5        $ txtMedicamentos��50   "   $
��0,10q�51      $ Medicamento��52      #��[  53      $	��398��6<   $��16(�6H   {�.  6V   54      �� 55      $ 
Horizontal=�6!   $	��400��6:   $��5,8,8,8q�6F   .+�56      �� -   5        $ txtIndicaciones��56   "   $
��0,10q�57      $ Indicaciones��58      #��[  59      $	��398��6<   $��16(�6H   {�.  6V   5:      5;      �� 5<      $��-420,10,8,8q�6   .+�5=      �� -   5        $ txtBuscarReceta��5=      $ Buscar Recetas��5>      #��[  5?      $	��543��6@   $��14(�6L   5@      �� 5A      $ Vertical=�6   $��-200,8,0,20q�64   $	��780��6I   .+�5B      &   System.Windows.Controls.DataGrid  -   5        
  Name$
 	dgRecetas��5B         AutoGenerateColumns .     
IsReadOnly .    Columns 5C      0  *System.Windows.Controls.DataGridTextColumn  5D      ,  &System.Windows.Controls.DataGridColumn   Header$ Código Receta��61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6H   �� *
RecetaID+  5E      $ Fecha Emision��61    6H   �� *FechaEmision+  5F      $ Carnet Paciente��61    6J   �� *
PacienteID+  5G      $ Carnet Médico��61    6H   �� *
MedicoID+  5H      $ Código Consulta��61    6J   �� *
ConsultaID+  5I      $ Estado��61    6A   �� *NombreEstadoReceta+5K      5M      5O      5R      �� 5S   
   $��2��6   $��	0,3,0,638q�6(   ��5T      �� 5U       �� 
StartPoint$ 0.5,0&�6&    �� EndPoint$ 0.5,1&�69   .��5V      �  � Color$ #F0F3FA��6#   � 5W      $ #D5DEEF��6#    � Offset$ 1Q�63   � 5X      $ #B1C9EF��6#   $	 0.5Q�63   5Y      5[      �� 5\   
   $ 
Horizontal=�6   $
��Left=�6.   $
��1000��6I   $��2��6V   $��
0,-710,0,0q�6j   .+�5]      �� -	   5        $��btnPacientes��5]      $��	Pacientes��6)   ���#395886
�������6f   �� 5^      -
   5        $��btnHistorialesMedicos��5^      $��Historiales Médicos��62   ���#395886
�������6y   �� 5_      -   5        $��
btnMedicos��5_      $��Médicos��6'   ���#395886
�������6b   �� 5`      -   5        $��btnConsultas��5`      $��	Consultas��6)   ���#395886
�������6f   �� 5a      -   5        $��
btnRecetas��5a      $��Recetas��6'   ���#D5DEEF
�������6b   �� 5b      -   5        $��btnCitas��5b      $��Citas��6&   ���#395886
�������6_   �� 5c      -   5        $��btnExamenes��5c      $��Examenes��6(   ���#395886
�������6d   �� 5d      -   5        $��btnReportes��5d      $��Reportes��6(   ���#395886
�������6P   �� 5e      -   5         ��Name$ btnUsuarios��5e      $��Usuarios��6*   ���#395886
�������6f   5f      �� 5g   
   $��Registro de Recetas��6   $
��Left=�5h      $	��Top=�5i      $��40(�5j      $��	10,35,0,0q�5k      
���   �$��2��5l      � 5m   
   .+�5n      �� $��0,209,-884,0q�6   $	��522��6/   $	��Top=�6<   $��Right=�6T   $	��234��6p   .+�5p      �� -   5        $ btnCancelar��5p      $��50��6.   $��90��6:   
�������
������� � ToolTip$ Cancelar��6l   .��5q      �� $��Assets/actualizar.png��6   $��60��6;   5r      �� 5s      -   5        $ btnEntregarReceta��5s      $��50��64   $��90��6@   
�������
�������$ Entegar Receta��6r   .��5t      �� $��Assets/entregar.png��6   $��60��69   5u      �� 5v      -   5        $ btnEliminarReceta��5v      $��50��64   $��90��6@   
�������
�������$ Entegar Receta��6r   .��5w      �� $��Assets/delete.png��6   $��60��67   5x      5y      �� 5{      $��0,430,-915,0q�6   $	��280��6/   $	��Top=�6<   $��Right=�6T   $	��300��6p   .+�5|        -   5        $
 dgMedicamentos��5|       .  $	��300��5~       .  5�        5�      $ ID��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ 46 6\    69   �� *MedicamentosID+6\     5�      $ Medicamentos��6-   $	 190 6i    6C   �� *NombreMedicamento+6i     5�      $
 Ind.��6-   $ 65 6\    6;   �� *Indicaciones+6\   5�      5�      5�      � 5�   
   .+�5�      �� -   5        $ btnHome��5�      $��50��6&   
�������$��974,9,-906,691q�6E   
�������.��5�      �� $��Assets/regresar.png��6   $��50��65   $��60��6A   5�      5�      �� 5�   
   -   5        $ btnRegresar��5�      $��50��6&   ���#395886$��708,130,79,570q�6G   
�������$��REGRESAR��6s   $
��Bold"�6�   $��16(�6�   $��1��6�   5�      5�      5�      