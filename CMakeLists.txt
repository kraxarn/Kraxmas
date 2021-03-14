cmake_minimum_required(VERSION 3.5)
project(open-td CXX)

# C++ 11
set(CMAKE_CXX_STANDARD 11)

# Main source and header files
file(GLOB MAIN_SRC "src/*.[hc]pp")
add_executable(open-td ${MAIN_SRC})

target_link_libraries(open-td PUBLIC open-td-res)

# crowengine
add_subdirectory(engine)
target_link_libraries(open-td PRIVATE crowengine)

# Wii U build
if (DEFINED DEVKITPRO)
	include("${DEVKITPRO}/wut/share/wut.cmake" REQUIRED)
	wut_create_rpx(open-td)
endif ()