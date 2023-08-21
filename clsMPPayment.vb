Imports MySql.Data.MySqlClient
Imports ADODB
Imports ggcAppDriver
Imports CrystalDecisions.CrystalReports.Engine

Public Class clsMPPayment
    Private p_oDriver As ggcAppDriver.GRider
    Private p_oSTRept As DataSet
    Private p_oDTSrce As DataTable

    Private p_nReptType As Integer      '0=Summary;1=Detail
    Private p_abInclude(1) As Integer   '0=Default;1=Financing
    Private p_dDateFrom As Date
    Private p_dDateThru As Date
    Private p_sBranchCD As String

    Public Function getParameter() As Boolean
        Dim loFrm As frmReportMenu

        loFrm = New frmReportMenu

        'Disable Report Type Group
        loFrm.gbxPanel01.Enabled = False
        'Set Detail as Report Type
        loFrm.rbtTypex02.Checked = True

        loFrm.ShowDialog()

        If loFrm.isOkey Then
            'Since we have not allowed the report type to be edited
            p_nReptType = 0

            p_abInclude(0) = loFrm.chkInclude01.Checked
            p_abInclude(1) = loFrm.chkInclude02.Checked

            p_dDateFrom = loFrm.txtField01.Text
            p_dDateThru = loFrm.txtField02.Text

            loFrm = Nothing
            Return True
        Else
            loFrm = Nothing
            Return False
        End If
    End Function

    Public Function ReportTrans() As Boolean
        Dim oProg As frmProgress

        Dim lsSQL As String 'whole statement
        Dim lsQuery1 As String
        Dim lsQuery2 As String


        'Show progress bar
        oProg = New frmProgress
        oProg.PistonInfo = p_oDriver.AppPath & "/piston.avi"
        oProg.ShowTitle("EXTRACTING RECORDS FROM DATABASE")
        oProg.ShowProcess("Please wait...")
        oProg.Show()


        lsQuery1 = "SELECT a.sAcctNmbr 'sAcctNmbr'" & _
                    ",CONCAT(b.sLastName,',', b.sFrstName,' ',b.sMiddName)AS sClientNm" & _
                    ",c.sReferNox 'sReferNox'" & _
                    ",c.nAmountxx 'nAmountxx'" & _
                    ",c.nIntAmtxx 'nIntAmtxx'" & _
             " FROM MC_AR_Master a" & _
                 " LEFT JOIN Client_Master b" & _
                     " ON a.sClientID = b.sClientID" & _
              " LEFT JOIN LR_Payment_Master c" & _
                  " ON b.sClientID = c.sClientID" & _
              " WHERE c.dTransact BETWEEN " & strParm(Format(p_dDateFrom, "yyyy-MM-dd") & " 00:00:00") & _
                  "AND" & strParm(Format(p_dDateThru, "yyyy-MM-dd") & " 23:59:00") & _
                    "AND a.cLoanType = '4'" & _
                    "AND c.sSourceCD <> 'CChk'" & _
                    " AND c.cPostedxx = " & strParm(xeTranStat.TRANS_POSTED)

        lsQuery2 = "SELECT a.sAcctNmbr 'sAcctNmbr'" & _
                   ",CONCAT(b.sLastName,',', b.sFrstName,' ',b.sMiddName)AS sClientNm" & _
                   ",c.sReferNox 'sReferNox'" & _
                   ",c.nAmountxx 'nAmountxx'" & _
                   ",c.nIntAmtxx 'nIntAmtxx'" & _
            " FROM MC_AR_Master a" & _
                " LEFT JOIN Client_Master b" & _
                    " ON a.sClientID = b.sClientID" & _
             " LEFT JOIN LR_Payment_Master_PR c" & _
                 " ON b.sClientID = c.sClientID" & _
             " WHERE c.dTransact BETWEEN " & strParm(Format(p_dDateFrom, "yyyy-MM-dd") & " 00:00:00") & _
                 "AND" & strParm(Format(p_dDateThru, "yyyy-MM-dd") & " 23:59:00") & _
                   "AND a.cLoanType = '4'" & _
                   " AND c.cPostedxx = " & strParm(xeTranStat.TRANS_POSTED)

        'If lsSQL <> "" Then
        '    lsSQL = lsSQL & ""
        'End If

        lsSQL = ""
        If p_abInclude(0) Then
            lsSQL = lsQuery1
        End If

        If p_abInclude(1) And lsSQL <> "" Then
            lsSQL = lsQuery1 & " UNION " & lsQuery2
        ElseIf p_abInclude(1) Then
            lsSQL = lsQuery2
        End If
        Debug.Print(lsSQL)

        p_oDTSrce = p_oDriver.ExecuteQuery(lsSQL)

        Dim loDtaTbl As DataTable = getRptTable()
        Dim lnCtr As Integer

        oProg.ShowTitle("LOADING RECORDS")
        oProg.MaxValue = p_oDTSrce.Rows.Count

        For lnCtr = 0 To p_oDTSrce.Rows.Count - 1

            oProg.ShowProcess("Loading " & p_oDTSrce(lnCtr).Item("sClientNm") & "...")

            loDtaTbl.Rows.Add(addRow(lnCtr, loDtaTbl))
        Next

        oProg.ShowSuccess()

        Dim clsRpt As clsMPReports
        clsRpt = New clsMPReports
        clsRpt.GRider = p_oDriver

        'Set the Report Source Here
        If Not clsRpt.initReport("TLMC1") Then
            Return False
        End If

        Dim loRpt As ReportDocument = clsRpt.ReportSource

        Dim loTxtObj As CrystalDecisions.CrystalReports.Engine.TextObject
        loTxtObj = loRpt.ReportDefinition.Sections(0).ReportObjects("txtCompany")
        loTxtObj.Text = p_oDriver.BranchName

        'Set Branch Address
        loTxtObj = loRpt.ReportDefinition.Sections(0).ReportObjects("txtAddress")
        loTxtObj.Text = p_oDriver.Address & vbCrLf & p_oDriver.TownCity & " " & p_oDriver.ZippCode & vbCrLf & p_oDriver.Province

        'Set First Header
        loTxtObj = loRpt.ReportDefinition.Sections(1).ReportObjects("txtHeading1")
        loTxtObj.Text = "Mobile Phone Payment Transaction"

        'Set Second Header
        loTxtObj = loRpt.ReportDefinition.Sections(1).ReportObjects("txtHeading2")
        loTxtObj.Text = Format(p_dDateFrom, "MMMM dd yyyy") & " to " & Format(p_dDateThru, "MMMM dd yyyy")

        loTxtObj = loRpt.ReportDefinition.Sections(3).ReportObjects("txtRptUser")
        loTxtObj.Text = Decrypt(p_oDriver.UserName, "08220326")

        loRpt.SetDataSource(p_oSTRept)
        clsRpt.showReport()

        Return True
    End Function

    Private Function getRptTable() As DataTable
        'Initialize DataSet
        p_oSTRept = New DataSet

        'Load the data structure of the Dataset
        'Data structure was saved at DataSet1.xsd 
        p_oSTRept.ReadXmlSchema(p_oDriver.AppPath & "\vb.net\Reports\DataSet1.xsd")

        'Return the schema of the datatable derive from the DataSet 
        Return p_oSTRept.Tables(0)
    End Function

    Private Function addRow(ByVal lnRow As Integer, ByVal foSchemaTable As DataTable) As DataRow
        'ByVal foDTInclue As DataTable
        Dim loDtaRow As DataRow

        'Create row based on the schema of foSchemaTable
        loDtaRow = foSchemaTable.NewRow

        loDtaRow.Item("nField01") = lnRow + 1
        loDtaRow.Item("sField02") = p_oDTSrce(lnRow).Item("sAcctNmbr")
        loDtaRow.Item("sField03") = p_oDTSrce(lnRow).Item("sClientNm")
        loDtaRow.Item("sField05") = p_oDTSrce(lnRow).Item("sReferNox")
        loDtaRow.Item("lField01") = p_oDTSrce(lnRow).Item("nAmountxx")
        loDtaRow.Item("lField02") = p_oDTSrce(lnRow).Item("nIntAmtxx")

        Return loDtaRow
    End Function

    Private Function getAgent(ByVal sAgentIDx As String) As String
        Dim lsSQL As String

        lsSQL = "SELECT sUserName FROM xxxSysUser WHERE sUserIDxx = " & strParm(sAgentIDx)

        Dim loDT As DataTable
        loDT = p_oDriver.ExecuteQuery(lsSQL)

        If loDT.Rows.Count = 0 Then Return ""

        Return Decrypt(loDT(0)("sUserName"), "08220326")
    End Function

    Public Sub New(ByVal foRider As GRider)
        p_oDriver = foRider
        p_oSTRept = Nothing
        p_oDTSrce = Nothing
    End Sub
End Class

