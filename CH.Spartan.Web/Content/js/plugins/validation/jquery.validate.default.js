jQuery.validator.setDefaults(
   {
       highlight: function (e) {
           $(e).closest(".form-group").removeClass("has-success").addClass("has-error");
       },
       success: function (e) {
           e.closest(".form-group").removeClass("has-error").addClass("has-success");
       },
       errorElement: "span",
       errorPlacement: function (e, r) {
           e.appendTo(r.is(":radio") || r.is(":checkbox") ? r.parent().parent().parent() : r.parent());
       },
       errorClass: "help-block m-b-none",
       validClass: "help-block m-b-none"
   });

// �����֤ 
jQuery.validator.addMethod("money", function (value, element) {
    var tel = /^([+-]?)((\d{1,3}(,\d{3})*)|(\d+))(\.\d{2})?$/;
    return this.optional(element) || (tel.test(value));
});

// �ֻ�������֤ 
jQuery.validator.addMethod("mobile", function (value, element) {
    var length = value.length;
    var mobile = /^(((13[0-9]{1})|(15[0-9]{1}))+\d{8})$/;
    return this.optional(element) || (length == 11 && mobile.test(value));
});

// �绰������֤ 
jQuery.validator.addMethod("phone", function (value, element) {
    var tel = /^(0[0-9]{2,3}\-)?([2-9][0-9]{6,7})+(\-[0-9]{1,4})?$/;
    return this.optional(element) || (tel.test(value));
});

// ����������֤ 
jQuery.validator.addMethod("zipCode", function (value, element) {
    var tel = /^[0-9]{6}$/;
    return this.optional(element) || (tel.test(value));
});

// QQ������֤ 
jQuery.validator.addMethod("qq", function (value, element) {
    var tel = /^[1-9]\d{4,9}$/;
    return this.optional(element) || (tel.test(value));
});

// IP��ַ��֤ 
jQuery.validator.addMethod("ip", function (value, element) {
    var ip = /^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$/;
    return this.optional(element) || (ip.test(value) && (RegExp.$1 < 256 && RegExp.$2 < 256 && RegExp.$3 < 256 && RegExp.$4 < 256));
});

// ��ĸ�����ֵ���֤ 
jQuery.validator.addMethod("chrnum", function (value, element) {
    var chrnum = /^([a-zA-Z0-9]+)$/;
    return this.optional(element) || (chrnum.test(value));
});

// ���ĵ���֤ 
jQuery.validator.addMethod("chinese", function (value, element) {
    var chinese = /^[\u4e00-\u9fa5]+$/;
    return this.optional(element) || (chinese.test(value));
});

// ��������֤ 
$.validator.addMethod("selectNone", function (value, element) {
    return value == "��ѡ��";
});

// �ֽڳ�����֤ 
jQuery.validator.addMethod("byteRangeLength", function (value, element, param) {
    var length = value.length;
    for (var i = 0; i < value.length; i++) {
        if (value.charCodeAt(i) > 127) {
            length++;
        }
    }
    return this.optional(element) || (length >= param[0] && length <= param[1]);
});