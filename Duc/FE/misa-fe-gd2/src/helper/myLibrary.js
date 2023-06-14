/**
* tạo đường dẫn ảnh
* CreatedBy: Trịnh Huỳnh Đức (12-6-2023)
*/
export function buildImage(nameImage) {
   if (nameImage != '00000000-0000-0000-0000-000000000000' && nameImage ) {
       const image = `https://localhost:44393/api/v1/Images?imageName=${nameImage}`
       return image
   }
   return null
}
export const myLibrary = {
    methods: {
      buildImage
    }
  }