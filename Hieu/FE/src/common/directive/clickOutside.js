export default {
    /**
     * Directive clickOutside
     * @param {} el 
     * @param {*} binding 
     * CreatedBy VMHieu 31/03/2023
     */
      mounted(el, binding) {
          el.clickOutsideEvent = function (event) {
            if (!(el === event.target || el.contains(event.target))) {
              binding.value();
            }
          };
          document.addEventListener("click", el.clickOutsideEvent, true);
        },
      unmounted(el) {
      document.removeEventListener("click", el.clickOutsideEvent, true);
      },
  };