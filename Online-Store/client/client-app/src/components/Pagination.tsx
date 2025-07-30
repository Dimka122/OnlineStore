interface PaginationProps {
  currentPage: number;
}

export const Pagination = ({ currentPage }: PaginationProps) => {
  return <div>Пагинация (страница {currentPage})</div>;
};