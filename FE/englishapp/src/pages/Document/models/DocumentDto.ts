interface DocumentDto {
    name: string,
    documentSize: number,
    description?: string,
    createdAt: Date,
    classId: number,
    className: string,
    id: number,
}
export default DocumentDto;