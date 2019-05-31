var Common = ({
    Control: {
        Loading: "#loadingPanel",
    },
    Variable: {
        Success: 'Success',
        Error: 'Error',
        Info: 'Info',
        Warning: 'Warning',
        DeleteConfirm: "Are you sure, you want to delete this {0} ?",
        IsActiveConfirm: "Are you sure, you want to {0} this {1} ?",
        StatusChangeMsg: "Are you sure, you want to Change Status ?",
        IsCheckReload: false,
        TooShort: "Too short",
        Weak: "Weak",
        Good: "Good",
        Strong: "Strong",
        KuwaitDialingCode: "+965",
        Active: "1",
        InActive: "3",
    },
    URL: {
        TempFilePath: "/ProjectFiles/Temp/",
        GetCountryList: "/Common/GetCoutryList",
        GetCityListByCountryId: "/Common/GetCityListByCountryId",
        GetCategoryList: "/Common/GetCategoryList",
        GetSubCategoryListByCategoryId: "/Common/GetSubCategoryListByCategoryId",
        GetItemListBySubCategoryId: "/Common/GetItemListBySubCategoryId",
        GetCityList: "/Common/GetCityList",
        GetAllBusinessOwnerList: "/Common/GetAllBusinessOwnerList",
        GetAllCustomerList: "/Common/GetAllCustomerList",
        AccountLogin: "/Account/Login",
        GetPaymentTypeList: "/Common/GetPaymentTypeList",
        GetCountryListWithDialingCode: "/Common/GetCountryListWithDialingCode",
        GetAdvertisementPositionList: "/Common/GetAdvertisementPositionList",
        GetLocationListByCityId: "/Common/GetLocationListByCityId",
    },
    GetData: function (url, inputdata, successmsg, failuremsg) {
        $.ajax({
            type: "GET",
            url: url,
            data: inputdata,
            success: function (data) {
                successmsg(data);
            },
            error: function (data) {
                failuremsg(data);
            }
        })
    },
    ShowLoading: function () {
        $(Common.Control.Loading).show();
    },
    HideLoading: function () {
        $(Common.Control.Loading).hide();
    },
    ShowToastrMessage: function (messageType, messageTitle, message) {
        toastr.options.positionClass = 'toast-top-full-width';
        toastr[messageType.toLowerCase()](message, messageTitle);
        Common.HideLoading();
    },
    ValidateImage: function (file) {
        var ext = file.split(".");
        ext = ext[ext.length - 1].toLowerCase();
        var arrayExtensions = ["jpg", "jpeg", "png", "bmp"];
        if (arrayExtensions.lastIndexOf(ext) == -1) {
            Common.ShowToastrMessage("Error", "Error", "Select Valid Image File !");
            return false;
        }
        return true;
    },
    ReplacementString: function (string, replacementArray) {
        return string.replace(/({\d})/g, function (j) {
            return replacementArray[j.replace(/{/, '').replace(/}/, '')];
        });
    },
    PostData: function (url, inputdata, successmsg, failuremsg) {
        $.ajax({
            type: "POST",
            url: url,
            data: inputdata,
            async: false,
            success: function (data) {
                successmsg(data);
            },
            error: function (data) {
                failuremsg(data);
            }
        })
    },
    PostDataStandard: function (url, inputdata, successmsg, failuremsg) {
        $.ajax({
            type: "POST",
            url: url,
            contentType: "application/json; chatset=utf-8",
            data: inputdata,
            success: function (data) {
                successmsg(data);
            },
            error: function (data) {
                failuremsg(data);
            }
        })
    },
    UploadPhoto: function (url, inputdata, successmsg, failuremsg) {
        $.ajax({
            url: url,
            type: "POST",
            processData: false,
            contentType: false,
            data: inputdata,
            success: function (data) {
                successmsg(data);
            },
            error: function (data) {
                failuremsg(data);
            }
        });
    },
    isNumericKey: function (evt, txtControl) {
        var charCode = (evt.which) ? evt.which : evt.keyCode

        if (charCode == 46) {
            var inputValue = $(txtControl).val()
            if (inputValue.indexOf('.') < 1) {
                return true;
            }
            return false;
        }
        if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 0) {
            return false;
        }
        return true;
    },
    isNumberKey: function (evt) {
        //var charCode = (evt.which) ? evt.which : evt.keyCode

        //if (charCode > 31 && (charCode < 48 || charCode > 57) && charCode != 0) {
        //    return false;
        //}
        //return true;
        return null;
    },
    isAlphabetCharacter: function (txt) {
        $(txt).val($(txt).val().replace(/[^A-Za-z ]/g, ''));
    }
});

$(document).ready(function () {
    $('.txtNumber').on('input', function (event) {
        this.value = this.value.replace(/[^0-9]/g, '');
    });
    $('.txtDecimal').on('input', function (event) {
        var val = $(this).val();
        if (isNaN(val)) {
            val = val.replace(/[^0-9\.]/g, '');
            if (val.split('.').length > 1)
                val = val.replace(/\.+$/, "");
        }
        $(this).val(val);
    });
    $(".select_filter, .text_filter").addClass("form-control");
});