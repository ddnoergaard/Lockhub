function toggleMobileNav() {
    const mobileNav = document.querySelector('.hidden-mobile-nav');

    let overlay = document.querySelector('.nav-overlay');
    if (!overlay) {
        overlay = document.createElement('div');
        overlay.classList.add('nav-overlay');
        overlay.addEventListener('click', toggleMobileNav);
        document.body.appendChild(overlay);
    }

    mobileNav.classList.toggle('nav-open');
    overlay.classList.toggle('overlay-active');
}

document.querySelector('.nav-list-mobile').addEventListener('click', toggleMobileNav);
document.querySelector('.hidden-mobile-nav img').addEventListener('click', toggleMobileNav);

function toggleServices(e) {
    e.preventDefault();
    console.log("test")
    document.querySelector('.mobile-services-on-hover').classList.toggle('services-open');
}