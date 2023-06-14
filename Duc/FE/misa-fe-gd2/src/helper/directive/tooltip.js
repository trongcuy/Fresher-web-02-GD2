const tooltipDirective = {
    mounted(el, binding) {
        const tooltipText = binding.value;
        //lấy mode của tooltip
        const mode = binding.arg;

        if (!tooltipText)
            return

        // Tạo một phần tử span cho tooltip
        const tooltip = document.createElement('span');
        tooltip.className = 'tooltip';
        tooltip.innerText = tooltipText;

        // Ẩn tooltip ban đầu
        tooltip.style.visibility = 'hidden';

        // Xử lý sự kiện khi rê chuột ra khỏi phần tử chứa directive
        el.addEventListener('mouseleave', () => {
            tooltip.style.visibility = 'hidden';
        });

        // Xử lý sự kiện khi rê chuột vào phần tử chứa directive     
        el.addEventListener('mouseenter', () => {
            tooltip.style.visibility = 'visible';
            if(mode == 'left') {
                const tooltipWidth = tooltip.offsetWidth
                tooltip.style.left = `-${tooltipWidth + 16}px`
                tooltip.style.top = `4px`
            }
            if(mode == 'top') {
                const tooltipWidth = tooltip.tooltipWidth
                tooltip.style.left = `-${tooltipWidth/2}px`
                tooltip.style.top = `-40px`
            }
            if(mode == 'bottom') {
                const tooltipWidth = tooltip.tooltipWidth
                tooltip.style.left = `-${tooltipWidth/2}px`
                tooltip.style.top = `40px`
            }
        });

        // Thêm tooltip vào phần tử chứa directive
        el.appendChild(tooltip);
    },
};

export default tooltipDirective;