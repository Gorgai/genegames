export function setItem(key, value) {
    localStorage.setItem(key, value);
}

export function getItem(key) {
    return localStorage.getItem(key);
}

export function preventDefaultContextMenu(element) {
    if (element instanceof HTMLElement) {
        element.addEventListener("contextmenu", function (e) {
            e.preventDefault();
        });
    } else {
        console.error("Invalid element passed to preventDefaultContextMenu");
    }
}

window.addKeyListener = (dotNetObject) => {
    document.addEventListener('keydown', (event) => {
        if (event.key === 'r' || event.key === 'R') {
            dotNetObject.invokeMethodAsync('ResetGame');
        }
    });
};
