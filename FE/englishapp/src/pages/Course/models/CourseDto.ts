interface CourseDto {
    id: number,
    name: string,
    teacherName: string,
    className: string,
    lessonName: string // Buổi học
    teacherId: number,
    lessonId: number,
    startDated: Date,
    packageType: number
}
export default CourseDto;