   M S B A M L   `   `   `  ����B  >Usuario, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null!clr-namespace:UsuarioUsuario  5   	   #    Usuario.frmHistorialPacientes   5   	   n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.4000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   'localclr-namespace:Usuario     5   	   b ^MaterialDesignThemes.Wpf, Version=4.1.0.2556, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   -   5          � Title$  Historial de Pacientes��5	   	   $	��750��6(   $
��1100��65    � 
ResizeMode$ NoResize=�6B    � WindowStartupLocation$ CenterScreen=�6X    � WindowStyle$
 None=�6}   .��5
      �  � ColumnDefinitions 5   
   �� 5      $	��33* �6   �� 5      $
��167* �6   .+�5   
   �� $��
0,60,126,0q�6   $��20q�6'   $��2��64   .��5      �� $��	0,0,0,-19q�6   $	��980��6,   .+�5      �  5      �� 5      $��* �6+   �� 5      $��* �6+   .+�5      �� $��0��6!   $��0,0,5,0q�61   .+�5      �� -   5         ��Name$ txtPacienteID��5   "   $
��0,10q�5      )  #MaterialDesignThemes.Wpf.HintAssist   Hint$ Carnet Paciente��5       %  !MaterialDesignFloatingHintTextBox#��[  5      �� 5       -   5        $ txtMotivoConsulta��5    "   $
��0,10q�5!      $ Motivo de la consulta��5"      #��[  5#      �� 5%      -   5        $ txtPadecimientos��5%   "   $
��0,10q�5&      $ Padecimientos��5'      #��[  5(      �� 5*      -   5        $ txtTraumatismos��5*   "   $
��0,10q�5+      $ Traumatismos��5,      #��[  5-      �� 5/      -   5        $ txtCirugiasPrevias��5/   "   $
��0,10q�50      $ Cirugias Previas��51      #��[  52      �� 54      -   5        $ txtMedicacionActual��54   "   $
��0,10q�55      $ Medicación Actual��56      #��[  57      58      �� 5;      $��1��6!   $��5,0,0,0q�61   .+�5<      �� -   5        $ txtAntecedentesFamiliares��5<   "   $
��0,10q�5=      $ Antecedentes Familiares��5>      #��[  5?      �� 5A      -	   5        $ txtDiscapacidad��5A   "   $
��0,10q�5B      $ Discapacidades��5C      #��[  5D      �� 5F      -
   5        $ 
txtAlergia��5F   "   $
��0,10q�5G      $ Alergias��5H      #��[  5I      �� 5K      -   5        $ txtEnfermedadCronica��5K   "   $
��0,10q�5L      $ Enfermedades Crónicas��5M      #��[  5N      �� 5P      -   5        $ txtObservaciones��5P   "   $
��0,10q�5Q      $ Observaciones��5R      #��[  5S      5T      5U      �� 5X      $
��0,10q�6   .+�5Y      �� -   5        $ 	txtBuscar��5Y      $ Buscar Codigo��5Z      #��[  5[      6I   5\      �� 5_       �� Orientation$ Vertical=�6   $
��0,20q�64   $	��205��6B   $	��976��6O   .+�5`      &   System.Windows.Controls.DataGrid  -   5          Name$ dgHistorial��5`      	   AutoGenerateColumns	 .  
   
IsReadOnly
 . $	��178��6b      Columns 5a      0  *System.Windows.Controls.DataGridTextColumn  5b      ,  &System.Windows.Controls.DataGridColumn   Header$ Codigo��61   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6A   �� *ID+  5c      $ id��61    6=   �� *
PacienteID+  5d      $ Nombre��61    6A   �� *Nombre+  5e      $ Apellido��61    6C   �� *
Apellido+  5f      $ Motivo Consulta��61    6J   �� *MotivoConsulta+  5g      $ Padecimientos��61    6H   �� *Padecimientos+  5h      $ Traumatismos��61    6G   �� *Traumatismos+  5i      $ Cirugias Previas��61    6K   �� *CirugiasPrevias+  5j      $ Medicacion Actual��61    6L   �� *MedicacionActual+  5k      $ Antecedentes Familiares��61    6R   �� *AntecedentesFamiliares+  5l      $ Discapacidad��61    6G   �� *Discapacidad+  5m      $ Alergia��61    6B   �� *	Alergia+  5n      $ Enfermedad Cronica��61    6M   �� *EnfermedadCronica+  5o      $ Observaciones��61    6H   �� *Observaciones+5q      5r      5s      5v      �� 5w   
   $��2��6   $��	0,3,0,638q�6(   ��5x      �� 5y       �� 
StartPoint$ 0.5,0&�6&    �� EndPoint$ 0.5,1&�69   .��5z      �  � Color$ #F0F3FA��6#   � 5{      $ #D5DEEF��6#    � Offset$ 1Q�63   � 5|      $ #B1C9EF��6#   $	 0.5Q�63   5}      5      �� 5�   
   $ 
Horizontal=�6   $
��Left=�6.   $
��1000��6I   $��2��6V   $��
0,-710,0,0q�6j   .+�5�      �� -   5         ��Name$ btnPacientes��5�      $��	Pacientes��6+   ���#395886
�������6h   �� 5�      -   5        $ btnHistorialesMedicos��5�      $��Historiales Médicos��64   ���#395886
�������6g   �� 5�      -   5        $ 
btnMedicos��5�      $��Médicos��6)   ���#395886
�������6d   �� 5�      -   5        $ btnConsultas��5�      $��	Consultas��6+   ���#395886
�������6h   �� 5�      -   5        $ 
btnRecetas��5�      $��Recetas��6)   ���#395886
�������6d   �� 5�      -   5        $ btnCitas��5�      $��Citas��6'   ���#395886
�������6`   �� 5�      -   5        $ btnExamenes��5�      $��Examenes��6*   ���#395886
�������6f   �� 5�      -   5        $ btnReportes��5�      $��Reportes��6*   ���#395886
�������6f   �� 5�      -   5        $ btnUsuarios��5�      $��Usuarios��6*   ���#395886
�������6f   5�      �� 5�   
   $��Historial Medico��6   $
��Left=�5�      $	��Top=�5�      $��40(�5�      $��	10,35,0,0q�5�      
���   �$��2��5�      � 5�   
   .+�5�      �� $��0,219,-984,0q�6   $	��522��6/   $	��Top=�6<   $��Right=�6T   $	��240��6p   .+�5�      �� -   5        $ 
btnAgregar��5�      $��50��6-   $��90��69   
�������
������� � ToolTip$ Guardar��6k   .��5�      �� $��Assets/newuser.png��6   $��60��68   5�      �� 5�      -   5        $ 
btnGuardar��5�      $��50��6-   $��90��69   
�������
�������$ Guardar��6k   .��5�      �� $��Assets/save.png��6   $��60��65   5�      �� 5�      -   5        $ 	btnEditar��5�      $��50��6,   $��90��68   
�������
�������$ Editar��6j   .��5�      �� $��Assets/edit.png��6   $��60��65   5�      �� 5�      -   5        $ btnCancelar��5�      $��50��6.   $��90��6:   
�������
�������$ Cancelar��6l   .��5�      �� $��Assets/actualizar.png��6   $��60��6;   5�      5�      5�      � 5�   
   .+�5�      �� -   5        $ btnHome��5�      $��50��6&   
�������$��974,9,-906,691q�6E   
�������.��5�      �� $��Assets/regresar.png��6   $��50��65   $��60��6A   5�      5�      5�      5�      