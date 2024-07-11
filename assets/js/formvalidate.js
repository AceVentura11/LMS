$(function () {
    $('#txt_anyotherclause_advance').hide();
    $('#txt_anyotherclause_1stinterim').hide();
    $('#txt_anyotherclause_2ndinterim').hide();
    $('#txt_anyotherclause_3rdinterim').hide();
    $('#txt_anyotherclause_balance').hide();
    $('#txt_anyotherclause_other').hide();
    $('#sec_brandedproducts').hide();
    $('#sec_nat').hide();
    $('#erclaimed_sec').hide();
    $('.cati').hide();
    $('.papi').hide();
    $('.secondary').hide();
    $('.web').hide();
    $('.clt').hide();
    $('.di').hide();
    $('.others').hide();
    $('.gd').hide();
    $('.observation').hide();
    $('.consumer').hide();
    $('#txt_typeofstudy').hide();
    $('#txt_iv_others').hide();
    $('#txt_dcmothers').hide();

    $("#txt_expectedfwstartdate").prop('disabled', true);
    $("#txt_expectedfwenddate").prop('disabled', true);
    $("#txt_expectedprojectenddate").prop('disabled', true);

    $("#txt_projectcommisioneddate").datepicker({
        minDate: 0,
        dateFormat: 'yy-mm-dd',
        changeMonth: true,
        onSelect: function (date) {
            var selectedDate = new Date(date);
            var msecsInADay = 86400000;
            var endDate = new Date(selectedDate.getTime() + msecsInADay);

            $("#txt_expectedfwstartdate").datepicker("option", "minDate", endDate);
            $("#txt_expectedfwstartdate").prop('disabled', false);
            $("#txt_expectedfwenddate").datepicker("option", "minDate", endDate);
            $("#txt_expectedprojectenddate").datepicker("option", "minDate", endDate);
        }
    });

    $("#txt_expectedfwstartdate").datepicker({
        minDate: 0,
        dateFormat: 'yy-mm-dd',
        changeMonth: true,
        onSelect: function (date1) {
            var selectedDate1 = new Date(date1);
            var msecsInADay1 = 86400000;
            var endDate1 = new Date(selectedDate1.getTime() + msecsInADay1);
            $("#txt_expectedfwenddate").datepicker("option", "minDate", endDate1);
            $("#txt_expectedfwenddate").prop('disabled', false);
            $("#txt_expectedprojectenddate").datepicker("option", "minDate", endDate1);
        }
    });

    $("#txt_expectedfwenddate").datepicker({
        minDate: 0,
        dateFormat: 'yy-mm-dd',
        changeMonth: true,
        onSelect: function (date2) {
            var selectedDate2 = new Date(date2);
            var msecsInADay2 = 86400000;
            var endDate2 = new Date(selectedDate2.getTime() + msecsInADay2);
            $("#txt_expectedprojectenddate").datepicker("option", "minDate", endDate2);
            $("#txt_expectedprojectenddate").prop('disabled', false);
        }
    });

    $("#txt_expectedprojectenddate").datepicker({
        minDate: 0,
        changeMonth: true,
        dateFormat: 'yy-mm-dd'
    });

    function validateope(id, type) {
        var quanti = 20;
        var quali = 22;
        var idf = 31;
        var cost = parseFloat($('#txt_jobvalue').val());

        if (cost <= 50000) {
            if (type == 'F') {
                quanti = 20; quali = 22; idf = 31;
            }
            else {
                quanti = 25; quali = 25; idf = 35;
            }
        }
        else if (cost > 50000 && cost <= 100000) {
            if (type == 'F') {
                quanti = 27; quali = 23; idf = 38;
            }
            else {
                quanti = 32; quali = 26; idf = 42
            }
        }
        else if (cost > 100000 && cost <= 200000) {
            if (type == 'F') {
                quanti = 30; quali = 24; idf = 40;
            }
            else {
                quanti = 35; quali = 27; idf = 44;
            }
        }
        else if (cost > 200000 && cost <= 500000) {
            if (type == 'F') {
                quanti = 33; quali = 26; idf = 43;
            }
            else {
                quanti = 38; quali = 29; idf = 47;
            }
        }
        else if (cost > 500000 && cost <= 1000000) {
            if (type == 'F') {
                quanti = 38; quali = 28; idf = 47;
            }
            else {
                quanti = 44; quali = 32; idf = 51;
            }
        }
        else if (cost > 1000000 && cost <= 2000000) {
            if (type == 'F') {
                quanti = 42; quali = 30; idf = 51;
            }
            else {
                quanti = 48; quali = 33; idf = 56;
            }
        }
        else if (cost > 2000000 && cost <= 4000000) {
            if (type == 'F') {
                quanti = 46; quali = 32; idf = 55;
            }
            else {
                quanti = 53; quali = 36; idf = 60;
            }
        }
        else if (cost > 4000000) {
            if (type == 'F') {
                quanti = 50; quali = 50; idf = 50;
            }
            else {
                quanti = 57; quali = 57; idf = 57
            }
        }

        return (parseFloat($('#' + id).val()) < quanti);
    }

    jQuery.validator.addMethod("fieldope", function () {
        return validateope('txt_fieldper', 'F');
    }, "Field OPE exceeds the limit");

    jQuery.validator.addMethod("totalope", function () {
        return validateope('txt_totalper', 'T');
    }, "Total OPE exceeds the limit");

    jQuery.validator.addMethod("DataCollectionMethod", function () {
        var ret = true;
        if ($('#dcm_1').prop('checked') == false && $('#dcm_2').prop('checked') == false && $('#dcm_3').prop('checked') == false && $('#dcm_4').prop('checked') == false &&
            $('#dcm_7').prop('checked') == false && $('#dcm_8').prop('checked') == false && $('#dcm_5').prop('checked') == false && $('#dcm_6').prop('checked') == false &&
            $('#dcm_9').prop('checked') == false && $('#dcm_10').prop('checked') == false && $('#dcm_11').prop('checked') == false) {
            ret = false;
        }
        return ret;
    }, "Please select Data Collection Method.");

    jQuery.validator.addMethod("paymentterms", function () {
        var sum = 0;
        var ret = true;
        $('.pt').each(function () {
            if (!isNaN(this.value) && this.value.length != 0) {
                sum += parseFloat(this.value);
            }
        });
        console.log(sum)
        if (sum != 100) {
            ret = false;
        }
        return ret;
    }, "Payment % not Adding to 100%");

    jQuery.validator.addMethod("CAPI", function () {
        var ret = true;
        if ($('#dcm_1').is(':checked') == false && ($('#txt_equipivalue').val() != "" || parseFloat($('#txt_equipivalue').val() > 0)))
        {
            ret = false;
        }
        return ret;
    }, "Please remove OPE on CAPI for non CAPI project");

    jQuery.validator.addMethod("CATI", function () {
        var ret = true;
        if ($('#dcm_2').is(':checked') == false && ($('#txt_cativalue').val() != "" || parseFloat($('#txt_cativalue').val() > 0))) {
            ret = false;
        }
        return ret;
    }, "Please remove OPE on CATI for non CATI project");

    $("#form1").validate(
    {
        // Rules for form validation
        rules:
        {
            txt_jobname:
            {
                required: true,
                minlength: 2,
                maxlength: 150
            },
            txt_fieldper: {
                fieldope: true
            },
            txt_totalper: {
                totalope: true
            },
            txt_teamhead:
            {
                required: true,
                minlength: 1,
                maxlength: 50
            },
            txt_researcher:
            {
                required: true,
                minlength: 1,
                maxlength: 50
            },
            ddl_originatingcentre:
            {
                required: true
            },
            dcm_11:
            {
                DataCollectionMethod: true
            },
            ddl_projecttype:
            {
                required: true
            },
            ddl_industryverticals:
            {
                required: true
            },
            ddl_typeofstudy:
            {
                required: true
            },
            ddl_nat:
            {
                required: true
            },
            txt_clientname:
            {
                required: true,
                minlength: 1,
                maxlength: 150
            },
            txt_clientaddress:
            {
                required: true,
                minlength: 1,
                maxlength: 500
            },
            txt_clientcontactpersonname:
            {
                required: true,
                minlength: 1,
                maxlength: 50
            },
            txt_clientcontactpersonemailid:
            {
                required: true,
                email: true,
                minlength: 1,
                maxlength: 60
            },
            txt_clientconatctpersonnumber:
            {
                required: true,
                minlength: 6,
                maxlength: 14
            },
            txt_clientaccountspersonname:
            {
                required: true,
                minlength: 1,
                maxlength: 50
            },
            txt_clientaccountspersonemailid:
            {
                required: true,
                email: true,
                minlength: 1,
                maxlength: 60
            },
            txt_clientaccountspersonnumber:
            {
                required: true,
                minlength: 6,
                maxlength: 14
            },
            txt_briefdescofstudy:
            {
                required: true,
                minlength: 1,
                maxlength: 500
            },
            txt_projectcommisioneddate:
            {
                required: true,
                date: true,
                minlength: 10,
                maxlength: 10
            },
            txt_expectedfwstartdate:
            {
                required: true,
                date: true,
                minlength: 10,
                maxlength: 10
            },
            txt_expectedfwenddate:
            {
                required: true,
                date: true,
                minlength: 10,
                maxlength: 10
            },
            txt_expectedprojectenddate:
            {
                required: true,
                date: true,
                minlength: 10,
                maxlength: 10
            },
            txt_totalsamplesize:
            {
                required: true,
                minlength: 1,
                maxlength: 10
            },
            ddl_fieldworkcentres:
            {
                required: true
            },
            ddl_fieldworkcentresIOB:
            {
                required: true
            },
            txt_fieldworkcentreslist:
            {
                required: true,
                minlength: 3,
                maxlength: 150
            },
            ddl_currency:
            {
                required: true
            },
            txt_amount:
            {
                required: true,
                minlength: 1,
                maxlength: 20
            },
            txt_jcr2:
            {
                required: true
            },
            txt_jobvalue:
            {
                required: true
            },
            txt_agreementclause:
            {
                required: true
            },
            ddl_typeoftrack:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#ddl_nat").val() == '2' || $("#ddl_nat").val() == '4');
                    }
                }
            },
            ddl_brandedproducts:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#chk_products').is(':checked') == true);
                    }
                }
            },
            txt_expensetobecharged1:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#chk_actualsclaimed').is(':checked') == true);
                    }
                }
            },
            txt_estimatedamount1:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#txt_expensetobecharged1").val() != '');
                    }
                }
            },
            txt_estimatedamount2:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#txt_expensetobecharged2").val() != '');
                    }
                }
            },
            txt_estimatedamount3:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#txt_expensetobecharged3").val() != '');
                    }
                }
            },
            txt_expensetobecharged1:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#txt_estimatedamount1").val() != '' || $('#chk_actualsclaimed').is(':checked') == true);
                    }
                }
            },
            txt_expensetobecharged2:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#txt_estimatedamount2").val() != '');
                    }
                }
            },
            txt_expensetobecharged3:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#txt_expensetobecharged3").val() != '');
                    }
                }
            },
            txt_anyotherclause_advance:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#ddl_advanceopt").val() == 'Others');
                    }
                }
            },
            txt_anyotherclause_1stinterim:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#ddl_1stinterim").val() == 'Others');
                    }
                }
            },
            txt_anyotherclause_2ndinterim:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#ddl_2ndinterim").val() == 'Others');
                    }
                }
            },
            txt_anyotherclause_3rdinterim:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#ddl_3rdinterim").val() == 'Others');
                    }
                }
            },
            txt_anyotherclause_balance:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#ddl_balance").val() == 'Others');
                    }
                }
            },
            txt_anyotherclause_other:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#ddl_anyotherclause").val() == 'Others');
                    }
                }
            },
            txt_advance:
            {
                required: true,
                digits: true
            },
            txt_1stinterim:
            {
                digits: true
            },
            txt_2ndinterim:
            {
                digits: true
            },
            txt_3rdinterim:
            {
                digits: true
            },
            txt_balance:
            {
                digits: true
            },
            txt_anyotherclause:
            {
                digits: true,
                paymentterms: true
            },
            ddl_advanceopt:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#txt_advance").val() != '0');
                    }
                }
            },
            ddl_1stinterim:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#txt_1stinterim").val() != '0');
                    }
                }
            },
            ddl_2ndinterim:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#txt_2ndinterim").val() != '0');
                    }
                }
            },
            ddl_3rdinterim:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#txt_3rdinterim").val() != '0');
                    }
                }
            },
            ddl_balance:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#txt_balance").val() != '0');
                    }
                }
            },
            ddl_anyotherclause:
            {
                required:
                {
                    depends: function (element) {
                        return ($("#txt_anyotherclause").val() != '0');
                    }
                }
            },
            txt_capiss:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_1').is(':checked') == true);
                    }
                }
            },
            txt_catiss:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_2').is(':checked') == true);
                    }
                }
            },
            txt_papiss:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_3').is(':checked') == true);
                    }
                }
            },
            txt_desksearchss:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_4').is(':checked') == true);
                    }
                }
            },
            txt_webss:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_5').is(':checked') == true);
                    }
                }
            },
            txt_cltss:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_6').is(':checked') == true);
                    }
                }
            },
            txt_gdss:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_9').is(':checked') == true);
                    }
                }
            },
            txt_observstionss:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_10').is(':checked') == true);
                    }
                }
            },
            txt_consumerss:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_11').is(':checked') == true);
                    }
                }
            },
            txt_othersss:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_8').is(':checked') == true);
                    }
                }
            },
            txt_diss:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_7').is(':checked') == true);
                    }
                }
            },
            txt_capicpc:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_1').is(':checked') == true);
                    }
                }
            },
            txt_caticpc:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_2').is(':checked') == true);
                    }
                }
            },
            txt_papicpc:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_3').is(':checked') == true);
                    }
                }
            },
            txt_desksearchcpc:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_4').is(':checked') == true);
                    }
                }
            },
            txt_webcpc:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_5').is(':checked') == true);
                    }
                }
            },
            txt_cltcpc:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_6').is(':checked') == true);
                    }
                }
            },
            txt_gdcpc:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_9').is(':checked') == true);
                    }
                }
            },
            txt_observstioncpc:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_10').is(':checked') == true);
                    }
                }
            },
            txt_consumercpc:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_11').is(':checked') == true);
                    }
                }
            },
            txt_otherscpc:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_8').is(':checked') == true);
                    }
                }
            },
            txt_dicpc:
            {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_7').is(':checked') == true);
                    }
                }
            },
            txt_fieldvalue: {
                required: true
            },
            txt_analysisvalue: {
                required: true
            },
            txt_researchvalue: {
                required: true
            },
            txt_equipivalue: {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_1').is(':checked') == true);
                    }
                },
                CAPI:true
            },
            txt_cativalue: {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_2').is(':checked') == true);
                    }
                },
                CATI:true
            },
            txt_iv_others: {
                required:
                {
                    depends: function (element) {
                        return ($("#ddl_industryverticals").val() != '0');
                    }
                }
            },
            txt_typeofstudy: {
                required:
                {
                    depends: function (element) {
                        return ($("#ddl_typeofstudy").val() != '0');
                    }
                }
            },
            txt_dcmothers: {
                required:
                {
                    depends: function (element) {
                        return ($('#dcm_8').is(':checked') == true);
                    }
                }
            }
        },
        errorPlacement: function (error, element) {
            error.insertAfter(element.parent());
        }
    });

    $('#chk_products').click(function () {
        if ($('#chk_products').is(':checked') == true) {
            $('#sec_brandedproducts').show();
        }
        else {
            $('#sec_brandedproducts').hide();
        }
    });

    $('#chk_actualsclaimed').click(function () {
        if ($('#chk_actualsclaimed').is(':checked') == true) {
            $('#erclaimed_sec').show();
        }
        else {
            $('#erclaimed_sec').hide();
        }
    });

    $('#ddl_nat').change(function () {
        var selectedVal = $('#ddl_nat option:selected').attr('value');
        if (selectedVal == 'Customised Track' || selectedVal == 'Syndicated Track') {
            $('#sec_nat').show();
        }
        else {
            $('#sec_nat').hide();
        }
    });

    $('#ddl_currency').change(function () {
        $('#txt_amount').val('');
        $('#txt_jcr1').val('');
        $('#txt_jcr2').val('');
        $('#txt_jobvalue').val('');
    });

    $("#txt_amount").change(function () {
        if ($('#ddl_currency').val() != 'Rs.') {
            $('#txt_jcr1').val($('#ddl_currency').val() + ' =');
            $('#txt_jcr2').focus();
        }
        else {
            $('#txt_jcr1').val('RS =');
            $('#txt_jobvalue').val($('#txt_amount').val());
            $('#txt_jcr2').val('1');
            $('#txt_fieldvalue').focus();
        }
    });

    /* Validate if empty */
    $('#txt_jcr2').change(function () {
        $('#txt_jobvalue').val($('#txt_amount').val() * $('#txt_jcr2').val());
        $('#txt_fieldvalue').focus();
    });

    $('#txt_fieldvalue').change(function () {
        var fieldvalue = $('#txt_fieldvalue').val();
        if (fieldvalue == "") { fieldvalue = "0"; }
        var equipivalue = $('#txt_equipivalue').val();
        if (equipivalue == "") { equipivalue = "0"; }
        var cativalue = $('#txt_cativalue').val();
        if (cativalue == "") { cativalue = "0"; }
        var analysisvalue = $('#txt_analysisvalue').val();
        if (analysisvalue == "") { analysisvalue = "0"; }
        var researchvalue = $('#txt_researchvalue').val();
        if (researchvalue == "") { researchvalue = "0"; }       

        var n = fieldvalue / $('#txt_jobvalue').val() * 100;
        $('#txt_fieldper').val(n.toFixed(2));
        $('#txt_totalvalue').val(parseInt(fieldvalue) + parseInt(equipivalue) + parseInt(cativalue) + parseInt(analysisvalue) + parseInt(researchvalue));
        setTimeout(function () {
            var fieldper = $('#txt_fieldper').val();
            if (fieldper == "") { fieldper = "0"; }
            var equipiper = $('#txt_equipiper').val();
            if (equipiper == "") { equipiper = "0"; }
            var catiper = $('#txt_catiper').val();
            if (catiper == "") { catiper = "0"; }
            var analysisper = $('#txt_analysisper').val();
            if (analysisper == "") { analysisper = "0"; }
            var researchper = $('#txt_researchper').val();
            if (researchper == "") { researchper = "0"; }
            $('#txt_totalper').val(parseFloat(fieldper) + parseFloat(equipiper) + parseFloat(catiper) + parseFloat(analysisper) + parseFloat(researchper));
        }, 500);
    });

    $('#txt_equipivalue').change(function () {
        var fieldvalue = $('#txt_fieldvalue').val();
        if (fieldvalue == "") { fieldvalue = "0"; }
        var equipivalue = $('#txt_equipivalue').val();
        if (equipivalue == "") { equipivalue = "0"; }
        var cativalue = $('#txt_cativalue').val();
        if (cativalue == "") { cativalue = "0"; }
        var analysisvalue = $('#txt_analysisvalue').val();
        if (analysisvalue == "") { analysisvalue = "0"; }
        var researchvalue = $('#txt_researchvalue').val();
        if (researchvalue == "") { researchvalue = "0"; }

        var n = equipivalue / $('#txt_jobvalue').val() * 100;
        $('#txt_equipiper').val(n.toFixed(2));
        $('#txt_totalvalue').val(parseInt(fieldvalue) + parseInt(equipivalue) + parseInt(cativalue) + parseInt(analysisvalue) + parseInt(researchvalue));
        setTimeout(function () {
            var fieldper = $('#txt_fieldper').val();
            if (fieldper == "") { fieldper = "0"; }
            var equipiper = $('#txt_equipiper').val();
            if (equipiper == "") { equipiper = "0"; }
            var catiper = $('#txt_catiper').val();
            if (catiper == "") { catiper = "0"; }
            var analysisper = $('#txt_analysisper').val();
            if (analysisper == "") { analysisper = "0"; }
            var researchper = $('#txt_researchper').val();
            if (researchper == "") { researchper = "0"; }
            $('#txt_totalper').val(parseFloat(fieldper) + parseFloat(equipiper) + parseFloat(catiper) + parseFloat(analysisper) + parseFloat(researchper));
        }, 500);
    });

    $('#txt_cativalue').change(function () {

        var fieldvalue = $('#txt_fieldvalue').val();
        if (fieldvalue == "") { fieldvalue = "0"; }
        var equipivalue = $('#txt_equipivalue').val();
        if (equipivalue == "") { equipivalue = "0"; }
        var cativalue = $('#txt_cativalue').val();
        if (cativalue == "") { cativalue = "0"; }
        var analysisvalue = $('#txt_analysisvalue').val();
        if (analysisvalue == "") { analysisvalue = "0"; }
        var researchvalue = $('#txt_researchvalue').val();
        if (researchvalue == "") { researchvalue = "0"; }

        var n = cativalue / $('#txt_jobvalue').val() * 100;
        $('#txt_catiper').val(n.toFixed(2));
        $('#txt_totalvalue').val(parseInt(fieldvalue) + parseInt(equipivalue) + parseInt(cativalue) + parseInt(analysisvalue) + parseInt(researchvalue));
        setTimeout(function () {
            var fieldper = $('#txt_fieldper').val();
            if (fieldper == "") { fieldper = "0"; }
            var equipiper = $('#txt_equipiper').val();
            if (equipiper == "") { equipiper = "0"; }
            var catiper = $('#txt_catiper').val();
            if (catiper == "") { catiper = "0"; }
            var analysisper = $('#txt_analysisper').val();
            if (analysisper == "") { analysisper = "0"; }
            var researchper = $('#txt_researchper').val();
            if (researchper == "") { researchper = "0"; }
            $('#txt_totalper').val(parseFloat(fieldper) + parseFloat(equipiper) + parseFloat(catiper) + parseFloat(analysisper) + parseFloat(researchper));
        }, 500);
    });

    $('#txt_analysisvalue').change(function () {

        var fieldvalue = $('#txt_fieldvalue').val();
        if (fieldvalue == "") { fieldvalue = "0"; }
        var equipivalue = $('#txt_equipivalue').val();
        if (equipivalue == "") { equipivalue = "0"; }
        var cativalue = $('#txt_cativalue').val();
        if (cativalue == "") { cativalue = "0"; }
        var analysisvalue = $('#txt_analysisvalue').val();
        if (analysisvalue == "") { analysisvalue = "0"; }
        var researchvalue = $('#txt_researchvalue').val();
        if (researchvalue == "") { researchvalue = "0"; }

        var n = analysisvalue / $('#txt_jobvalue').val() * 100;
        $('#txt_analysisper').val(n.toFixed(2));
        $('#txt_totalvalue').val(parseInt(fieldvalue) + parseInt(equipivalue) + parseInt(cativalue) + parseInt(analysisvalue) + parseInt(researchvalue));
        setTimeout(function () {
            var fieldper = $('#txt_fieldper').val();
            if (fieldper == "") { fieldper = "0"; }
            var equipiper = $('#txt_equipiper').val();
            if (equipiper == "") { equipiper = "0"; }
            var catiper = $('#txt_catiper').val();
            if (catiper == "") { catiper = "0"; }
            var analysisper = $('#txt_analysisper').val();
            if (analysisper == "") { analysisper = "0"; }
            var researchper = $('#txt_researchper').val();
            if (researchper == "") { researchper = "0"; }
            $('#txt_totalper').val(parseFloat(fieldper) + parseFloat(equipiper) + parseFloat(catiper) + parseFloat(analysisper) + parseFloat(researchper));
        }, 500);
    });

    $('#txt_researchvalue').change(function () {

        var fieldvalue = $('#txt_fieldvalue').val();
        if (fieldvalue == "") { fieldvalue = "0"; }
        var equipivalue = $('#txt_equipivalue').val();
        if (equipivalue == "") { equipivalue = "0"; }
        var cativalue = $('#txt_cativalue').val();
        if (cativalue == "") { cativalue = "0"; }
        var analysisvalue = $('#txt_analysisvalue').val();
        if (analysisvalue == "") { analysisvalue = "0"; }
        var researchvalue = $('#txt_researchvalue').val();
        if (researchvalue == "") { researchvalue = "0"; }        

        var n = researchvalue / $('#txt_jobvalue').val() * 100;
        $('#txt_researchper').val(n.toFixed(2));
        $('#txt_totalvalue').val(parseInt(fieldvalue) + parseInt(equipivalue) + parseInt(cativalue) + parseInt(analysisvalue) + parseInt(researchvalue));
        setTimeout(function () {
            var fieldper = $('#txt_fieldper').val();
            if (fieldper == "") { fieldper = "0"; }
            var equipiper = $('#txt_equipiper').val();
            if (equipiper == "") { equipiper = "0"; }
            var catiper = $('#txt_catiper').val();
            if (catiper == "") { catiper = "0"; }
            var analysisper = $('#txt_analysisper').val();
            if (analysisper == "") { analysisper = "0"; }
            var researchper = $('#txt_researchper').val();
            if (researchper == "") { researchper = "0"; }
            $('#txt_totalper').val((parseFloat(fieldper) + parseFloat(equipiper) + parseFloat(catiper) + parseFloat(analysisper) + parseFloat(researchper)).toFixed(2));
        }, 500);
    });
    /* Validate if empty */

    $('#ddl_advanceopt').change(function () {
        if ($('#ddl_advanceopt').val() == 'Others')
            $('#txt_anyotherclause_advance').show();
        else
            $('#txt_anyotherclause_advance').hide();
    });

    $('#ddl_1stinterim').change(function () {
        if ($('#ddl_1stinterim').val() == 'Others')
            $('#txt_anyotherclause_1stinterim').show();
        else
            $('#txt_anyotherclause_1stinterim').hide();
    });

    $('#ddl_2ndinterim').change(function () {
        if ($('#ddl_2ndinterim').val() == 'Others')
            $('#txt_anyotherclause_2ndinterim').show();
        else
            $('#txt_anyotherclause_2ndinterim').hide();
    });

    $('#ddl_3rdinterim').change(function () {
        if ($('#ddl_3rdinterim').val() == 'Others')
            $('#txt_anyotherclause_3rdinterim').show();
        else
            $('#txt_anyotherclause_3rdinterim').hide();
    });

    $('#ddl_balance').change(function () {
        if ($('#ddl_balance').val() == 'Others')
            $('#txt_anyotherclause_balance').show();
        else
            $('#txt_anyotherclause_balance').hide();
    });

    $('#ddl_anyotherclause').change(function () {
        if ($('#ddl_anyotherclause').val() == 'Others')
            $('#txt_anyotherclause_other').show();
        else
            $('#txt_anyotherclause_other').hide();
    });

    $('#dcm_1').change(function () {
        if ($('#dcm_1').prop('checked') == false) {
            $('.capi').hide();
        }
        else {
            $('.capi').show();
        }
    });

    $('#dcm_2').change(function () {
        if ($('#dcm_2').prop('checked') == false) {
            $('.cati').hide();
        }
        else {
            $('.cati').show();
        }
    });

    $('#dcm_3').change(function () {
        if ($('#dcm_3').prop('checked') == false) {
            $('.papi').hide();
        }
        else {
            $('.papi').show();
        }
    });

    $('#dcm_4').change(function () {
        if ($('#dcm_4').prop('checked') == false) {
            $('.secondary').hide();
        }
        else {
            $('.secondary').show();
        }
    });

    $('#dcm_5').change(function () {
        if ($('#dcm_5').prop('checked') == false) {
            $('.web').hide();
        }
        else {
            $('.web').show();
        }
    });

    $('#dcm_6').change(function () {
        if ($('#dcm_6').prop('checked') == false) {
            $('.clt').hide();
        }
        else {
            $('.clt').show();
        }
    });

    $('#dcm_7').change(function () {
        if ($('#dcm_7').prop('checked') == false) {
            $('.di').hide();
        }
        else {
            $('.di').show();
        }
    });

    $('#dcm_8').change(function () {
        if ($('#dcm_8').prop('checked') == false) {
            $('.others').hide();
            $('#txt_dcmothers').hide();
        }
        else {
            $('.others').show();
            $('#txt_dcmothers').show();
            $('#txt_dcmothers').val('');
            $('#lbl_others').text('');
        }
    });

    $('#dcm_9').change(function () {
        if ($('#dcm_9').prop('checked') == false) {
            $('.gd').hide();
        }
        else {
            $('.gd').show();
        }
    });

    $('#dcm_10').change(function () {
        if ($('#dcm_10').prop('checked') == false) {
            $('.observation').hide();
        }
        else {
            $('.observation').show();
        }
    });

    $('#dcm_11').change(function () {
        if ($('#dcm_11').prop('checked') == false) {
            $('.consumer').hide();
        }
        else {
            $('.consumer').show();
        }
    });

    $('#ddl_typeofstudy').change(function () {
        if ($('#ddl_typeofstudy').val() == 'Others')
            $('#txt_typeofstudy').show();
        else
            $('#txt_typeofstudy').hide();
    });

    $('#ddl_industryverticals').change(function () {
        if ($('#ddl_industryverticals').val() == 'OTHS')
            $('#txt_iv_others').show();
        else
            $('#txt_iv_others').hide();
    });

    $('#ddl_billingclient').change(function () {
        var id = $('#ddl_billingclient').val();
        $.ajax({
            type: "POST",
            url: "Index.aspx/GetClientDetails",
            data: '{uid: "' + id + '" }',
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: OnSuccess,
            failure: function (response) {

            }
        });

        function OnSuccess(response) {
            var d = response.d.split('|');
            $('#txt_clientname').val(d[0]);
            $('#txt_clientaddress').val(d[1]);
            $('#txt_clientcontactpersonemailid').val(d[2]);
            $('#txt_clientcontactpersonname').val(d[3]);
            $('#txt_clientconatctpersonnumber').val(d[4]);
        }
    });

    $("#txt_dcmothers").keypress(function () {
        setTimeout(function () { $("#lbl_others").text( "( " + $("#txt_dcmothers").val() + " )"); }, 500);        
    });
});