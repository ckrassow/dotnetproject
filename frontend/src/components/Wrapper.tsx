interface WrapperProps {
    children: React.ReactNode;
    className?: string;
  }

const Wrapper = ({ children, className }: WrapperProps) => {
  return (
    <div className={`${className} h-screen flex flex-col items-center overflow-y-auto`}>
      {children}
    </div>
  );
};

export default Wrapper;