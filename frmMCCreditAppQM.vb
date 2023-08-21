Imports MySql.Data.MySqlClient
Imports ggcAppDriver
Imports ggcGOCAS

Public Class frmMCCreditAppQM
    Private pnLoadx As Integer
    Private poControl As Control
    Private WithEvents p_oTrans As ggcGOCAS.GOCASApplication
    Private p_nEditMode As Integer
    Dim psIncome As Decimal
    Dim psExpense As Decimal

    Private Sub frmMCCreditAppQM_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Debug.Print("frmMCCreditAppQM_Activated")
        If pnLoadx = 1 Then
            Call newTransaction()
            txtField80.Text = IIf(IFNull(p_oTrans.Master("sTransNox"), "") = "", "", p_oTrans.Master("sTransNox"))
            txtField81.Text = IIf(IFNull(p_oTrans.Master("sClientNm"), "") = "", "", p_oTrans.Master("sClientNm"))
            initButton()
            pnLoadx = 2
        End If
    End Sub

    Private Sub frmMCCreditAppQM_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Debug.Print("frmMCCreditAppQM_Load")
        If pnLoadx = 0 Then
            p_oTrans = New ggcGOCAS.GOCASApplication(p_oAppDriver)

            'Set event Handlers for txtField
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "GotFocus", AddressOf txtField_GotFocus)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf txtField_KeyDown)
            Call grpEventHandler(Me, GetType(TextBox), "txtField", "LostFocus", AddressOf txtField_LostFocus)
            Call grpKeyHandler(Me, GetType(TextBox), "txtField", "KeyDown", AddressOf ArrowKeys_Keydown)
            Call grpEventHandler(Me, GetType(Button), "cmdButton", "Click", AddressOf cmdButton_Click)

            pnLoadx = 1
        End If
    End Sub

    Private Sub ClearFields(ByVal loControl As Control)
        Dim loTxt As Control
        Dim loIndex As Integer
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call ClearFields(loTxt)
            ElseIf (TypeOf loTxt Is TextBox) Then
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Then
                    Select Case loIndex
                        Case Else
                            loTxt.Text = "N/A"
                    End Select
                End If
            End If
        Next
        psIncome = 0
        psExpense = 0
    End Sub


    Private Sub txtField_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        poControl = loTxt

        loTxt.BackColor = Color.Azure
        loTxt.SelectAll()
    End Sub


    Private Sub txtField_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim loTxt As TextBox
        loTxt = CType(sender, System.Windows.Forms.TextBox)
        loTxt.BackColor = SystemColors.Window
    End Sub

    Private Sub cmdButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim loChk As Button
        loChk = CType(sender, System.Windows.Forms.Button)
        Dim lnIndex As Integer
        lnIndex = Val(Mid(loChk.Name, 10))

        Select Case lnIndex
            Case 3
                If p_oTrans.SearchTransaction("%", False) = True Then
                    loadEntry(Me.Panel1)
                    loadEntry(Me.Panel2)
                    initButton()
                End If
            Case 1
                lnMsg = MsgBox("Are you sure enough to disapparove " & _
                               vbCrLf + "this application??", vbYesNo + vbInformation, "Confirm")
                If lnMsg = vbYes Then
                    If p_oTrans.DisApproved Then
                        MsgBox("Application was successfully disapproved!!", vbInformation, "Notice")
                        newTransaction()
                    End If
                End If
            Case 4
                If p_oTrans.PostQuickMatch Then
                    MsgBox("Success...")
                End If

            Case 0 ' Exit
                Me.Dispose()
        End Select
    End Sub

    Private Sub ArrowKeys_Keydown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        Select Case e.KeyCode
            Case Keys.Up, Keys.Down, Keys.Left, Keys.Right
                Select Case e.KeyCode
                    Case Keys.Down, Keys.Right
                        SetNextFocus()
                    Case Keys.Up, Keys.Left
                        SetPreviousFocus()
                End Select
        End Select
    End Sub
    Private Sub txtField_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs)
        If e.KeyCode = Keys.F3 Or e.KeyCode = Keys.Enter Then
            Dim loTxt As Control
            loTxt = CType(sender, System.Windows.Forms.TextBox)

            loTxt = Nothing
            If TypeOf poControl Is TextBox Then
                loTxt = CType(poControl, System.Windows.Forms.TextBox)
            ElseIf TypeOf poControl Is CheckBox Then
                loTxt = CType(poControl, System.Windows.Forms.CheckBox)
            ElseIf TypeOf poControl Is ComboBox Then
                loTxt = CType(poControl, System.Windows.Forms.ComboBox)
            End If

            '*********************
            Dim loIndex As Integer
            loIndex = Val(Mid(loTxt.Name, 9))

            If Mid(loTxt.Name, 1, 8) = "txtField" Then
                Select Case loIndex
                    Case 80, 81
                        If p_oTrans.SearchTransaction(poControl.Text, IIf(loIndex = 0, True, False)) = True Then
                            loadEntry(Me.Panel1)
                            loadEntry(Me.Panel2)
                        End If
                End Select
            End If
            '*********************
            If TypeOf poControl Is TextBox Then
                SelectNextControl(loTxt, True, True, True, True)
            End If
        End If
    End Sub

    Public Sub initButton()
        Dim lbShow As Boolean

        lbShow = IIf(Trim(txtField12.Text) = "", False, True)

        'cmdButton04.Visible = Not lbShow
        'cmdButton05.Visible = lbShow
    End Sub

    Public Sub newTransaction()
        ClearFields(Me.Panel1)
        ClearFields(Me.Panel2)
        p_oTrans.NewTransaction()
    End Sub

    Private Sub loadEntry(ByVal loControl As Control)
        Dim loTxt As Control
        For Each loTxt In loControl.Controls
            If loTxt.HasChildren Then
                Call loadEntry(loTxt)
            ElseIf (TypeOf loTxt Is TextBox) Then
                Dim loIndex As Integer
                loIndex = Val(Mid(loTxt.Name, 9))
                If LCase(Mid(loTxt.Name, 1, 8)) = "txtField" Then
                    With p_oTrans.Detail
                        Select Case loIndex
                            Case 0
                                loTxt.Text = IFNull(p_oTrans.Master("sTransNox"), "N/A")
                            Case 1
                                loTxt.Text = p_oTrans.getBranch(IFNull(p_oTrans.Master("sBranchCd"), "N/A"), False, True, "")
                            Case 2
                                loTxt.Text = Format(IFNull(p_oTrans.Master("dTransact"), p_oAppDriver.getSysDate), xsDATE_MEDIUM)
                            Case 3
                                loTxt.Text = IIf(IsDate(p_oTrans.Detail.dTargetDt), Format(p_oTrans.Detail.dTargetDt, xsDATE_MEDIUM), "N/A")
                            Case 4
                                loTxt.Text = IFNull(p_oTrans.Master("sClientNm"), "")
                            Case 12
                                loTxt.Text = IFNull(p_oTrans.Master("sQMatchNo"), "N/A")
                            Case 14
                                loTxt.Text = Format(IFNull(p_oTrans.Master("nDownPaym"), 0), xsDECIMAL)
                            Case 21
                                loTxt.Text = IFNull(Format(CDate(p_oTrans.Detail.applicant_info.dBirthDte), xsDATE_MEDIUM), p_oAppDriver.getSysDate)
                            Case 22
                                loTxt.Text = Format(DateDiff("M", .applicant_info.dBirthDte, p_oAppDriver.getSysDate) / 12, "0.00") & " yrs"
                            Case 23
                                loTxt.Text = p_oTrans.getTownCity(IFNull(p_oTrans.Detail.applicant_info.sBirthPlc, "N/A"), False, True, "")
                            Case 20
                                loTxt.Text = p_oTrans.Detail.residence_info.present_address.sHouseNox + " " + vbCrLf & _
                                p_oTrans.getTownCity(p_oTrans.Detail.residence_info.present_address.sTownIDxx, False, True, "") + " " + vbCrLf & _
                                p_oTrans.getBarangay(p_oTrans.Detail.residence_info.present_address.sBrgyIDxx, False, True, "") + " " + vbCrLf & _
                                p_oTrans.Detail.residence_info.present_address.sAddress1
                            Case 24
                                If Not IsNothing(.means_info.cIncmeSrc) Then
                                    If Not IsNothing(.means_info.employed) Then
                                        loTxt.Text = IFNull(p_oTrans.getOccupation(.means_info.employed.sPosition, False, True, ""), "N/A")
                                    Else
                                        loTxt.Text = IFNull(.means_info.self_employed.sBusiness, "N/A")
                                    End If
                                End If
                            Case 25
                                loTxt.Text = p_oTrans.getModel(IFNull(p_oTrans.Detail.sModelIDx, "N/A"), False, True, "")
                            Case 26
                                loTxt.Text = IFNull(p_oTrans.Detail.nAcctTerm + " months", "N/A")
                            Case 27
                                loTxt.Text = IFNull(Format(CDbl(p_oTrans.Detail.nMonAmort), xsDECIMAL), 0)
                            Case 30
                                'Spouse(Information)
                                If Not IsNothing(.spouse_info) Then
                                    loTxt.Text = .spouse_info.personal_info.sLastName + "," + .spouse_info.personal_info.sFrstName
                                    txtField31.Text = p_oTrans.Detail.spouse_info.residence_info.present_address.sHouseNox + " " + vbCrLf & _
                                    p_oTrans.getTownCity(p_oTrans.Detail.spouse_info.residence_info.present_address.sTownIDxx, False, True, "") + " " + vbCrLf & _
                                    p_oTrans.getBarangay(p_oTrans.Detail.spouse_info.residence_info.present_address.sBrgyIDxx, False, True, "") + " " + vbCrLf & _
                                    p_oTrans.Detail.spouse_info.residence_info.present_address.sAddress1
                                    If Not IsNothing(.spouse_info.personal_info.dBirthDte) Then
                                        txtField32.Text = IIf(.spouse_info.personal_info.dBirthDte = "", "N/A", Format(CDate(p_oTrans.Detail.spouse_info.personal_info.dBirthDte), xsDATE_MEDIUM))
                                        txtField33.Text = Format(DateDiff("M", .spouse_info.personal_info.dBirthDte, p_oAppDriver.getSysDate) / 12, "0.00") & " yrs"
                                    End If
                                    txtField34.Text = p_oTrans.getTownCity(.spouse_info.personal_info.sBirthPlc, False, True, "")
                                    If Not IsNothing(.spouse_means.cIncmeSrc) Then
                                        If Not IsNothing(.spouse_means.employed) Then
                                            txtField35.Text = IFNull(p_oTrans.getOccupation(.spouse_means.employed.sPosition, False, True, ""), "N/A")
                                        Else
                                            txtField35.Text = IFNull(.spouse_means.self_employed.sBusiness, "N/A")
                                        End If
                                    End If
                                Else
                                    loTxt.Text = "N/A"
                                    txtField31.Text = "N/A"
                                    txtField32.Text = "N/A"
                                    txtField33.Text = "N/A"
                                    txtField34.Text = "N/A"
                                    txtField35.Text = "N/A"
                                End If
                            Case 80
                                    loTxt.Text = IFNull(p_oTrans.Master("sTransNox"), "")
                            Case 81
                                    loTxt.Text = IFNull(p_oTrans.Master("sClientNm"), "")
                        End Select
                        setCivilStat(.applicant_info.cCvilStat)
                        setApplicationType(.cApplType)
                        If Not IsDBNull(p_oTrans.Master("cTranStat")) Then setTransTat(p_oTrans.Master("cTranStat"))
                        If Not IsNothing(p_oTrans.Detail.means_info.financed) Then
                            chk00.Checked = IIf(p_oTrans.Detail.means_info.financed.sFinancer = "", 0, 1)
                        End If
                    End With
                End If
            End If
        Next
    End Sub

    Public Sub setCivilStat(ByVal sValue As String)
        Select Case sValue.ToLower
            Case "single"
                cmb03.SelectedIndex = 0
            Case "married"
                cmb03.SelectedIndex = 1
            Case "separated"
                cmb03.SelectedIndex = 2
            Case "widowed"
                cmb03.SelectedIndex = 3
            Case "single Parent"
                cmb03.SelectedIndex = 4
            Case "single parent with live in partner"
                cmb03.SelectedIndex = 5
            Case "0"
                cmb03.SelectedIndex = 0
            Case "1"
                cmb03.SelectedIndex = 1
            Case "2"
                cmb03.SelectedIndex = 2
            Case "3"
                cmb03.SelectedIndex = 3
            Case "4"
                cmb03.SelectedIndex = 4
            Case "5"
                cmb03.SelectedIndex = 5
            Case Else
                cmb03.SelectedIndex = 6
        End Select
    End Sub

    Public Sub setApplicationType(ByVal sValue As String)
        If Not IsNothing(sValue) Then
            Select Case sValue.ToLower
                Case "otorcycle"
                    cmb04.SelectedIndex = 0
                Case "sidecar"
                    cmb04.SelectedIndex = 1
                Case "others"
                    cmb04.SelectedIndex = 2
                Case "mobile phone"
                    cmb04.SelectedIndex = 3
                Case "cars"
                    cmb04.SelectedIndex = 4
                Case "services"
                    cmb04.SelectedIndex = 5
                Case "0"
                    cmb04.SelectedIndex = 0
                Case "1"
                    cmb04.SelectedIndex = 1
                Case "2"
                    cmb04.SelectedIndex = 2
                Case "3"
                    cmb04.SelectedIndex = 3
                Case "4"
                    cmb04.SelectedIndex = 4
                Case Else
                    cmb04.SelectedIndex = 5
            End Select

        End If
    End Sub

    Public Sub setTransTat(ByVal nStat As Integer)
        Select Case nStat
            Case 0
                PictureBox2.BackgroundImage = My.Resources.STAT_OPEN
            Case 1
                PictureBox2.BackgroundImage = My.Resources.STAT_CLOSED
            Case 2
                PictureBox2.BackgroundImage = My.Resources.STAT_POSTED
            Case 3
                PictureBox2.BackgroundImage = My.Resources.STAT_CANCELLED
            Case 4
                PictureBox2.BackgroundImage = My.Resources.STAT_VOID
            Case Else
                PictureBox2.BackgroundImage = My.Resources.STAT_UNKNOWN
        End Select
    End Sub
End Class