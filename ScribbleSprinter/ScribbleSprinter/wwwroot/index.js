
window.startFireworks = function () {
    const container = document.querySelector('.fireworks')
    const fireworks = new Fireworks.default(container)
    fireworks.start()
}
