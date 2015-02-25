function initTinyMCE() {
    if (navigator.sayswho != 'msie' )
    tinyMCE.init({
        selector: 'textarea.text-editor-html',
    theme: "advanced",
    mode: "textareas",
   
    theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,sub,sup,|,forecolor,backcolor,|,justifyleft,justifycenter,justifyright,justifyfull,|,formatselect,fontselect,fontsizeselect",
    theme_advanced_buttons2: "cut,copy,paste,|,undo,redo,|,bullist,numlist,|,outdent,indent,blockquote,|,link,unlink,anchor,image,|,charmap,hr,|,cleanup,removeformat,visualaid,toggle",
    theme_advanced_buttons3: "",
    theme_advanced_toolbar_location: "top",
    theme_advanced_toolbar_align: "left",
    theme_advanced_statusbar_location: "bottom",
    theme_advanced_resizing: true,
    theme_advanced_path: false,
    editor_selector: "text-editor-html",
    setup: function(ed) {
        ed.addButton('toggle', {
            title: 'Edit HTML Source',
            'class': 'mceIcon mce_code',
            onclick: function() {
                tinyMCE.execCommand('mceRemoveControl', false, ed.id);                
            }
        });
    }
});
}

//tinyMCE.init({
//    selector: 'textarea.text-editor-html',
//    theme: "advanced",
//    mode: "textareas",

//    theme_advanced_buttons1: "bold,italic,underline,strikethrough,|,sub,sup,|,forecolor,backcolor,|,justifyleft,justifycenter,justifyright,justifyfull,|,fontselect,fontsizeselect",
//    theme_advanced_buttons2: "cut,copy,paste,|,undo,redo,|,bullist,numlist,|,outdent,indent,blockquote,|,link,unlink,anchor,image,|,charmap,hr,|,cleanup,removeformat,visualaid",
//    theme_advanced_buttons3: "",
//    theme_advanced_toolbar_location: "top",
//    theme_advanced_toolbar_align: "left",
//    theme_advanced_statusbar_location: "bottom",
//    theme_advanced_resizing: true,
//    theme_advanced_path: false,
//    editor_selector: "text-editor-html",
//    setup: function (ed) {
//        ed.addButton('toggle', {
//            title: 'Edit HTML Source',
//            'class': 'mceIcon mce_code',
//            onclick: function () {
//                tinyMCE.execCommand('mceRemoveControl', false, ed.id);
//            }
//        });
//    }
//});

//tinyMCE.init({
//    theme: "advanced",
//    mode: "textareas",
//    plugins: "bbcode",
//    theme_advanced_buttons1: "bold,italic,underline,undo,redo,link,unlink,image,forecolor",
//    theme_advanced_buttons2: "",
//    theme_advanced_buttons3: "",
//    theme_advanced_toolbar_location: "top",
//    theme_advanced_toolbar_align: "left",
//    entity_encoding: "raw",
//    add_unload_trigger: true,
//    editor_selector: "text-editor-bbcode"
//});

//tinyMCE.init({
//    theme: "advanced",
//    mode: "textareas",
//    plugins: "bbcode",
//    entity_encoding: "raw",
//    add_unload_trigger: false,
//    readonly: true,
//    editor_selector: "text-bbcode"
//});