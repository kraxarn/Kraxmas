cmake_minimum_required(VERSION 3.5)
project(open-td CXX)

# C++ 11
set(CMAKE_CXX_STANDARD 11)

# SDL through pkg-config
find_package(PkgConfig REQUIRED)
pkg_check_modules(SDL2 REQUIRED sdl2)
pkg_check_modules(SDL2ttf REQUIRED SDL2_ttf)

# Header directories
include_directories(${SDL2_INCLUDE_DIRS})
include_directories(${SDL2ttf_INCLUDE_DIRS})

# Library directories
link_directories(${SDL2_LIBRARY_DIRS})
link_directories(${SDL2ttf_LIBRARY_DIRS})

# Resources
include(CMakeRC.cmake REQUIRED)
cmrc_add_resource_library(open-td-res
		NAMESPACE otd::res
		# font
		"res/fonts/kenney_blocks.ttf")

# Main source and header files
file(GLOB MAIN_SRC "src/*.[hc]pp")
add_executable(open-td ${MAIN_SRC})

# Linking
target_link_libraries(open-td PRIVATE ${SDL2_LIBRARIES} ${SDL2ttf_LIBRARIES})
target_link_libraries(open-td PUBLIC open-td-res)

# Wii U build
if (DEFINED DEVKITPRO)
	include("${DEVKITPRO}/wut/share/wut.cmake" REQUIRED)
	wut_create_rpx(open-td)
endif ()