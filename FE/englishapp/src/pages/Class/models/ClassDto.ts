interface ClassDto {
    id: number,
    name: string,
    description:string,
    courseName: string,
    courseId: number,
    teacherName: string,
    lessonName: string // Buổi học
    teacherId: number,
    lessonId: number,
    studentCount: number
}
export default ClassDto;