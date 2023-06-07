const tooltipDirective = {
    mounted(el, binding) {
        const tooltipText = binding.value;
        if(!tooltipText)
        return
        // Tạo một phần tử span cho tooltip
        const tooltip = document.createElement('span');
        tooltip.className = 'tooltip';
        tooltip.innerText = tooltipText;

        // Ẩn tooltip ban đầu
        tooltip.style.visibility = 'hidden';

        // Xử lý sự kiện khi rê chuột vào phần tử chứa directive
        el.addEventListener('mouseenter', () => {
            // Hiển thị tooltip
            tooltip.style.visibility = 'visible';
        });

        // Xử lý sự kiện khi rê chuột ra khỏi phần tử chứa directive
        el.addEventListener('mouseleave', () => {
            // Ẩn tooltip
            tooltip.style.visibility = 'hidden';
        });

        // Thêm tooltip vào phần tử chứa directive
        el.appendChild(tooltip);
    },
};

export default tooltipDirective;