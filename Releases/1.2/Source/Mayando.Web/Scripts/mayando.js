function toggleVisible(id) {
    var el = document.getElementById(id);
    el.style.display = (el.style.display != 'block' ? 'block' : 'none');
}

function setVisible(id, visible) {
    var el = document.getElementById(id);
    el.style.display = (visible ? 'block' : 'none');
}

function onMoveFormDoPost(id, direction) {
    var frm = document.getElementById("moveForm");
    frm.id.value = id;
    frm.direction.value = direction;
    frm.submit();
    return true;
}

function onBulkEditFormDoPost(action) {
    var frm = document.getElementById("bulkEditForm");
    frm.operation.value = action;
    frm.submit();
    return true;
}

function onBulkEditFormSetSelected(selected) {
    var frm = document.getElementById("bulkEditForm");
    for (i = 0; i < frm.length; i++) {
        if (frm[i].type == 'checkbox') {
            frm[i].checked = selected;
        }
    }
    return false;
}