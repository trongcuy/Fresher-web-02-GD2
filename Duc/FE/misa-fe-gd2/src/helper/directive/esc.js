export default {
    beforeMount(el, binding) {
        el._keydownCallback = (event) => {
            if (event.key === 'Escape') {
                binding.value()
          }
        }
        document.addEventListener('keydown', el._keydownCallback)
      },
      unmounted(el, binding) {
        document.removeEventListener('keydown', el._keydownCallback)
        delete el._keydownCallback
      }
};