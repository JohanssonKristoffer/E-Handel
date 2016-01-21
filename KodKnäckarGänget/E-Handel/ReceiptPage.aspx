<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReceiptPage.aspx.cs" Inherits="E_Handel.ReceiptPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="Style/ReceiptPage.css" rel="stylesheet" />
</head>
<body>
    <form id="ReceiptForm" runat="server">
        <div class="container-fluid">
            <div class="row-fluid">
                <div id="ReceiptOutput">
                    

                    <asp:Panel ID="ReceiptPanel" runat="server">
                        
                    </asp:Panel>

                    <div id="ReceiptOrderId">
                        <p>
                            Your order id is:
                            <p>
                                <label id="OrderIdlbl" runat="server"></label>
                    </div>

                    <div id="ReceiptTotalPrice">
                        <p>Total price: </p>
                        <label id="TotalPricelbl" runat="server"></label>
                    </div>

                    <div id="ReceiptVAT">
                        <p>Value added tax: </p>
                        <label id="VATlbl" runat="server"></label>
                    </div>

                    <div id="ReceiptPostage">
                        <p>Postage: </p>
                        <label id="Postagelbl" runat="server"></label>
                    </div>

                    <div id="ReceiptDeliveryAdress">
                        <p>Delivery Adress: </p>
                        <label id="Addresslbl" runat="server"></label>
                    </div>

                    <div id="ReceiptPostalCode">
                        <p>Postal code: </p>
                        <label id="PostalCodelbl" runat="server"></label>
                    </div>

                    <div id="ReceiptCity">
                        <p>City: </p>
                        <label id="Citylbl" runat="server"></label>
                    </div>

                    <div id="ReceiptCountry">
                        <p>Country: </p>
                        <label id="Countrylbl" runat="server"></label>
                    </div>

                    <div id="ReceiptEmail">
                        <p>Email adress: </p>
                        <label id="Emaillbl" runat="server"></label>
                    </div>

                    <div id="ReceiptPhoneNumber">
                        <p>Phone number: </p>
                        <label id="TelephoneNumberlbl" runat="server"></label>
                    </div>

                    <div id="ReceiptPaymentOptions">
                        <p>Payment option: </p>
                        <label id="PaymentOptionslbl" runat="server"></label>
                    </div>

                    <div id="ReceiptDeliveryOption">
                        <p>Delivery option: </p>
                        <label id="Deliveryoptionslbl" runat="server"></label>
                    </div>

                </div>

            </div>
        </div>
    </form>
</body>
</html>
