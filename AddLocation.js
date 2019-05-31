var LocationAdd = ({
    Control: {
        ddlCountry: "#CountryId",
        hfCountry: "#hfCountry",
        ddlCity: "#CityId",
        hfCity: "#hfCity",
        divLocationList: ".divLocationList",
        btnsave: ".btnsave",
        txtLocation: ".txtLocation",
        lblCity: "#lblCity",
        lblCountry: "#lblCountry",
        lblLocation: "#lblLocation",
        hfLocation: "#LocationID",
        lblLatitude: "#lblLatitude",
        txtLatitude: ".txtLatitude",
        lblLongitude: "#lblLongitude",
        txtLongitude: ".txtLongitude",
    },
    Variable: {
        CountryId: 0,
        CityId: 0,
    },
    URL: {
        GetLocationByCityIdandCountryId: "/Location/GetLocationByCityIdandCountryId",
        AddLocation: "/Location/AddLocation",
    },
    FillCountry: function () {
        Common.GetData(Common.URL.GetCountryList, null, LocationAdd.FillCountrySuccess, LocationAdd.FillCountryFail);
    },
    FillCountrySuccess: function (data) {
        var items = [];
        items.push("<option value=''>-- Select --</option>");
        $.each(data, function () {
            items.push("<option value=" + this.Value + ">" + this.Text + "</option>");
        });
        $(LocationAdd.Control.ddlCountry).html(items.join(' '));
        $(LocationAdd.Control.ddlCountry).val($(LocationAdd.Control.hfCountry).val());
    },
    FillCountryFail: function (data) {
        Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, data);
    },
    FillCity: function (cid) {
        $(LocationAdd.Control.ddlCity).val('');
        Common.ShowLoading();
        Common.GetData(Common.URL.GetCityListByCountryId + "?cid=" + cid, null, LocationAdd.FillCitySuccess, LocationAdd.FillCityFail);
    },
    FillCitySuccess: function (data) {
        var items = [];
        items.push("<option value=''>-- Select --</option>");
        $.each(data, function () {
            items.push("<option value=" + this.Value + ">" + this.Text + "</option>");
        });
        $(LocationAdd.Control.ddlCity).html(items.join(' '));
        $(LocationAdd.Control.ddlCity).val($(LocationAdd.Control.hfCity).val());
        Common.HideLoading();
    },
    FillCityFail: function (data) {
        Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, data);
    },
    FillLocationList: function () {
        LocationAdd.Variable.CountryId = 0;
        LocationAdd.Variable.CityId = 0;

        if ($(LocationAdd.Control.ddlCity).val() != "") {
            $(LocationAdd.Control.lblCity).text('');
        }
        if ($(LocationAdd.Control.ddlCountry).val() != null && $(LocationAdd.Control.ddlCountry).val() != "" && $(LocationAdd.Control.ddlCountry).val() != '0') {
            LocationAdd.Variable.CountryId = $(LocationAdd.Control.ddlCountry).val();
        }
        if ($(LocationAdd.Control.ddlCity).val() != null && $(LocationAdd.Control.ddlCity).val() != "" && $(LocationAdd.Control.ddlCity).val() != '0') {
            LocationAdd.Variable.CityId = $(LocationAdd.Control.ddlCity).val();
        }
        Common.ShowLoading();
        Common.GetData(LocationAdd.URL.GetLocationByCityIdandCountryId + "?CountryId=" + LocationAdd.Variable.CountryId + "&CityId=" + LocationAdd.Variable.CityId, null, LocationAdd.FillLocationListSuccess, LocationAdd.FillLocationListFail);
    },
    FillLocationListSuccess: function (data) {
        $(LocationAdd.Control.divLocationList).html(data);
        Common.HideLoading();
    },
    FillLocationListFail: function (data) {
        Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, data);
        Common.HideLoading();
    },
    SaveLocationModel: function () {
        Common.ShowLoading();
        $(LocationAdd.Control.lblCountry).text("");
        $(LocationAdd.Control.lblCity).text("");
        $(LocationAdd.Control.lblLocation).text("");
        $(LocationAdd.Control.lblLatitude).text("");
        $(LocationAdd.Control.lblLongitude).text("");

        var p_Location = {
            CityId: $(LocationAdd.Control.ddlCity).val(),
            LocationName: $(LocationAdd.Control.txtLocation).val(),
            LocationID: $(LocationAdd.Control.hfLocation).val(),
            Latitude: $(LocationAdd.Control.txtLatitude).val(),
            Longitude: $(LocationAdd.Control.txtLongitude).val(),
        };

        Common.PostDataStandard(LocationAdd.URL.AddLocation, JSON.stringify(p_Location), LocationAdd.SaveLocationSuccess, LocationAdd.SaveLocationFail)
    },
    SaveLocationSuccess: function (data) {
        var d = JSON.stringify(data);
        if (data.IsSuccess == false && data.Id == '0') {
            location.href = Common.URL.AccountLogin;
        }
        else {
            if (data.IsSuccess == false) {
                Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, data.Message);
            }
            else if (data != null) {
                $(LocationAdd.Control.divLocationList).html(data);
                Common.ShowToastrMessage(Common.Variable.Success, Common.Variable.Success, "Location has been saved successfully.");
                $(LocationAdd.Control.txtLocation).val('');
                $(LocationAdd.Control.txtLatitude).val('');
                $(LocationAdd.Control.txtLongitude).val('');
            }
        }
        Common.HideLoading();
    },
    SaveLocationFail: function (data) {
        Common.ShowToastrMessage(Common.Variable.Error, Common.Variable.Error, data);
        Common.HideLoading();
    },
});
$(document).ready(function () {
    LocationAdd.FillCountry();

    $(LocationAdd.Control.ddlCity).change(function () {
        LocationAdd.FillLocationList();
    });

    if ($(LocationAdd.Control.hfCountry).val() != undefined && $(LocationAdd.Control.hfCountry).val() != '0' && $(LocationAdd.Control.hfCountry).val() != '') {
        LocationAdd.FillCity($(LocationAdd.Control.hfCountry).val());
    }

    $(LocationAdd.Control.ddlCountry).change(function () {
        if ($(LocationAdd.Control.ddlCountry).val() != undefined && $(LocationAdd.Control.ddlCountry).val() != '0' && $(LocationAdd.Control.ddlCountry).val() != '') {
            if ($(LocationAdd.Control.ddlCountry).val() != "") {
                $(LocationAdd.Control.lblCountry).text('');
            }
            LocationAdd.FillCity($(LocationAdd.Control.ddlCountry).val());
        }
        else {
            LocationAdd.FillCity(0);
        }
        LocationAdd.FillLocationList();
    });

    $(document).on("click", LocationAdd.Control.btnsave, function () {
        $(LocationAdd.Control.lblCountry).text("");
        $(LocationAdd.Control.lblCity).text("");
        $(LocationAdd.Control.lblLocation).text("");
        $(LocationAdd.Control.lblLatitude).text("");
        $(LocationAdd.Control.lblLongitude).text("");

        var IsValid = true;

        if ($(LocationAdd.Control.txtLocation).val() == "") {
            $(LocationAdd.Control.lblLocation).text("Please enter Location.");
            IsValid = false;
        }

        if ($(LocationAdd.Control.ddlCity).val() == "") {
            $(LocationAdd.Control.lblCity).text("Please select City.");
            IsValid = false;
        }

        if ($(LocationAdd.Control.ddlCountry).val() == "") {
            $(LocationAdd.Control.lblCountry).text("Please select Country.");
            IsValid = false;
        }

        if ($(LocationAdd.Control.txtLatitude).val() == "") {
            $(LocationAdd.Control.lblLatitude).text("Please enter Latitude.");
            IsValid = false;
        }

        if ($(LocationAdd.Control.txtLongitude).val() == "") {
            $(LocationAdd.Control.lblLongitude).text("Please enter Longitude.");
            IsValid = false;
        }

        if (IsValid== true) {
            LocationAdd.SaveLocationModel();
        }
    });

})