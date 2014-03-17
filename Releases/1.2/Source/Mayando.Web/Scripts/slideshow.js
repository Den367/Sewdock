var gSlideshowDelay;
var gSlideshowNextUrl;
var gSlideshowTimeout;

function slideshowInitialize(delay, nextUrl) {
    gSlideshowDelay = delay;
    gSlideshowNextUrl = nextUrl;
    slideshowResume();
}

function slideshowPause() {
    if (gSlideshowTimeout) {
        clearTimeout(gSlideshowTimeout);
        gSlideshowTimeout = null;
    }
    else {
        slideshowResume();
    }
}

function slideshowResume() {
    if (gSlideshowNextUrl && !gSlideshowTimeout) {
        gSlideshowTimeout = setTimeout(slideshowAdvance, Math.abs(gSlideshowDelay * 1000));
    }

    hideElementIf("navigation-slideshow-backwards", (gSlideshowDelay < 0));
    hideElementIf("navigation-slideshow-forwards", (gSlideshowDelay > 0));
    hideElementIf("navigation-slideshow-pause", (!gSlideshowNextUrl));
    hideElementIf("navigation-slideshow-speedup", (Math.abs(gSlideshowDelay) <= 1));
}

function slideshowGoForwards() {
    if (gSlideshowDelay < 0) {
        slideshowInverse();
    }
    else {
        slideshowResume();
    }
}

function slideshowGoBackwards() {
    if (gSlideshowDelay > 0) {
        slideshowInverse()
    }
    else {
        slideshowResume();
    }
}

function slideshowInverse() {
    slideshowSetDelay(gSlideshowDelay * -1);
}

function slideshowSpeedDown() {
    var delay = gSlideshowDelay;
    var absDelay = Math.abs(delay);
    var sign = 1;
    if (delay != 0) {
        sign = absDelay / delay;
    }

    if (absDelay >= 1 && absDelay < 3) {
        absDelay = absDelay + 1;
    } else if (absDelay == 3) {
        absDelay = 5;
    } else if (absDelay == 10) {
        absDelay = 15;
    } else {
        absDelay = absDelay * 2;
    }
    slideshowSetDelay(sign * absDelay);
}

function slideshowSpeedUp() {
    var delay = gSlideshowDelay;
    var absDelay = Math.abs(delay);
    var sign = 1;
    if (delay != 0) {
        sign = absDelay / delay;
    }

    if (absDelay > 1 && absDelay <= 3) {
        absDelay = absDelay - 1;
    } else if (absDelay == 5) {
        absDelay = 3;
    } else if (absDelay == 15) {
        absDelay = 10;
    } else if (absDelay > 1) {
        absDelay = Math.round(absDelay / 2);
    }
    slideshowSetDelay(sign * absDelay);
}

function slideshowSetDelay(delay) {
    document.URL = document.URL.replace('slideshow=' + gSlideshowDelay, 'slideshow=' + delay);
}

function slideshowAdvance() {
    if (gSlideshowNextUrl) {
        document.URL = gSlideshowNextUrl;
    }
}

function hideElementIf(elementId, condition) {
    var element = document.getElementById(elementId);
    if (element) {
        if (condition) {
            element.style.display = "none";
        }
        else {
            element.style.display = "inline";
        }
    }
}