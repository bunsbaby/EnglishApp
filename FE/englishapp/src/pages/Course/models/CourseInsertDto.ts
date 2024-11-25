interface CourseInsertDto {
    name: string,
    className: string,
    description: string,
    teacherId: number,
    lessonId: number,
    packageType: number,
    startDated: Date
}
export default CourseInsertDto;