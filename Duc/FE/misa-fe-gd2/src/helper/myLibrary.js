/**
* tạo đường dẫn ảnh
* CreatedBy: Trịnh Huỳnh Đức (12-6-2023)
*/
function buildImage(nameImage) {
   if (nameImage != '00000000-0000-0000-0000-000000000000' && nameImage ) {
       const image = `https://localhost:44393/api/v1/Images?imageName=${nameImage}`
       return image
   }
   return null
}

/**
* validate ảnh
* CreatedBy: Trịnh Huỳnh Đức (15-6-2023)
*/
function validateImage(type) {
  if (type === 'image/png' || type === 'image/jpg' || type === 'image/jpeg') {
      return true
  }
  return false
}

/**
* validate file excel
* CreatedBy: Trịnh Huỳnh Đức (15-6-2023)
*/
function validateExcel(type) {
  if (type === 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet' || type === 'application/vnd.ms-excel') {
      return true
  }
  return false
}

export const myLibrary = {
    methods: {
      buildImage,
      validateImage,
      validateExcel
    }
  }